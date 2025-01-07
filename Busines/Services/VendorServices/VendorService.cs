using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Dataccess.Models.Entities;
using Qyrenx.Business.Models.DTOs.VendorDtos;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Business.DTOs.VendorDtos;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Business.Services.DeliveryServices;
using System.Text.Json;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.DTOs.VendorActiveDto;
using CloudinaryDotNet.Actions;
using Qyrenx.Business.DTOs;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Qyrenx.Business.Services.VendorServices
{
    public class VendorService : IVendorServices
	{
		private readonly QyrenxContext _context;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly ICloudinaryService _cloudinaryService;
		private readonly IJwtService _jwtService;
		private readonly IEmailServices _emailServices;
        private readonly IDbAccess _dbAccess;
        private readonly IDeliveryService _deliveryService;
        public VendorService(QyrenxContext context,ICloudinaryService cloudinaryService,IMapper mapper,IJwtService jwtService,IEmailServices emailServices,IDbAccess dbAccess,IDeliveryService deliveryService)  
        {
            _context = context;
			_mapper = mapper;
			_cloudinaryService= cloudinaryService;
			_jwtService=jwtService;
			_emailServices=emailServices;
            _dbAccess = dbAccess;
            _deliveryService = deliveryService;
        }
       

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendor()
		{
			var vendors = _context.Vendors.Include(v=>v.VendorAddress);
            var vendor = vendors.Select(v => new VendorAdminViewDto
            {
                Id = v.Id,
                Name = v.Name,
                ShopeName = v.ShopeName,
                ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
                Mobile = v.Mobile,
                Email = v.Email,
                IsBlock = v.IsBlock,
                City = v.VendorAddress.City,
                House = v.VendorAddress.House,
                LandMark = v.VendorAddress.LandMark,
                PostalCode = v.VendorAddress.PostalCode,
            });
			return vendor.ToList();
		}

		public async Task<VendorAdminViewDto> GetVendorById(Guid id)
		{
			try
			{
				var exist = await _context.Vendors.FirstOrDefaultAsync(e=>e.Id==id);
				if (exist == null)
					return new VendorAdminViewDto { };
				var vendor = _mapper.Map<VendorAdminViewDto>(exist);
				return vendor;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendorByShopeName(string name)
		{
			var vendors = _context.Vendors.Where(c => c.ShopeName.ToLower().Contains(name.ToLower()));
			if (!vendors.Any())
			{
				return Enumerable.Empty<VendorAdminViewDto>();
			}
			var vendor = vendors.Select(v => new VendorAdminViewDto
			{
				Id = v.Id,
				Name = v.Name,
				ShopeName = v.ShopeName,
				ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
				Mobile = v.Mobile,
				Email = v.Email,
                IsBlock= v.IsBlock,
			});
			return vendor.ToList();
		}

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendorNotVerified()
		{
			var vendors = _context.Vendors.Where(v => v.IsVerified == false);
			var vendor = vendors.Select(v => new VendorAdminViewDto
			{
				Id = v.Id,
				Name = v.Name,
				ShopeName = v.ShopeName,
				ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
				Mobile = v.Mobile,
				Email = v.Email,
                IsBlock= v.IsBlock,
			});
			return vendor.ToList();
		}

		public async Task<AllLoginresponses> LoginVendor(VendorLogin loginDto)
		{
			try
			{
				var exist = await _context.Vendors.SingleOrDefaultAsync(p => p.Email == loginDto.Email);
				if (exist != null)
				{
					if (BCrypt.Net.BCrypt.Verify(loginDto.Password, exist.HashPassword))
					{
						if (exist.IsVerified == true)
						{
							if (exist.IsBlock == false)
							{
								var token = _jwtService.GenerateJwt(exist.Id, exist.Email, exist.Role);
                                var refreshtoken=await _jwtService.CreaterefreshToken(exist.Id, exist.Email, exist.Role);
                                exist.RefreshToken = refreshtoken;
                                exist.TokenExpiryTime = DateTime.UtcNow.AddDays(30);
                                await _context.SaveChangesAsync();
                                return new AllLoginresponses{ Name = exist.Name, Id = exist.Id,Email=exist.Email,Role=exist.Role, Token = token ,refreshToken=refreshtoken};
							}
							return new AllLoginresponses { Error = "person is blocked " };
						}
						return new AllLoginresponses { Error = "persons verification is pending!" };
					}
					return new AllLoginresponses { Error = "enter valid credentials" };
				}
				return new AllLoginresponses { Error = "no such vendor person is registered" };
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message);
			}
		}

		public async Task<string> RegisterVendor(VendorRegisterDto registerDto, IFormFile shopelicense)
		{
			try
			{

				var exist = await _context.Vendors.FirstOrDefaultAsync(c => c.Email == registerDto.Email);
				if (exist != null)
				{
					return "vendor already exist";
				}
				bool emailverify = _emailServices.verifyOtp(registerDto.Email, registerDto.otp);
				if (emailverify)
				{
					var license = await _cloudinaryService.UploadDocumentAsync(shopelicense);
					var vendor = _mapper.Map<Vendor>(registerDto);
					vendor.HashPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
					vendor.ShopeLicense = license;
					_context.Vendors.Add(vendor);
                    await _context.SaveChangesAsync();
                    var vendoraddress=_mapper.Map<VendorAddress>(registerDto);
                    vendoraddress.Role = "Vendor";
                    var ven_id = await _context.Vendors.FirstOrDefaultAsync(v => v.Email == registerDto.Email);
                    vendoraddress.VendorId = ven_id.Id;
                    _context.VendorAddresses.Add(vendoraddress);
					await _context.SaveChangesAsync();
                    var data =await _dbAccess.GetAllVendor();
                    var data1=data.FirstOrDefault(p=>p.Email == registerDto.Email);
                    await VendorActivity(data1.Id);
					return "registered successfully";
				}
				return "wrong otp";
			}
			catch (Exception ex)
			{
				throw new Exception($"Registration Failed ,MSG {ex}");
			}
		}
		public async Task<bool> BlockVendor(Guid id)
		{
			try
			{
				var vendor = await _context.Vendors.FirstOrDefaultAsync(e=>e.Id==id);
				if (vendor == null)
				{
					return false;
				}
				vendor.IsBlock = true;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in block");
			}
		}
		public async Task<bool> UnblockVendor(Guid id)
		{
			try
			{
				var vendor = await _context.Vendors.FindAsync(id);
				if (vendor == null)
				{
					return false;
				}
				vendor.IsBlock = false;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in unblock");
			}
		}

		public async Task<bool> VerificationVendor(Guid id)
		{
			try
			{
				var vendor = await _context.Vendors.FindAsync(id);
				if (vendor == null)
				{
					return false;
				}
				vendor.IsVerified= true;
				await _context.SaveChangesAsync();
                await VendorActivity(id)    ;
				await _emailServices.SendVerifiedmsg(vendor.Role,vendor.Name,vendor.Email);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in VERIFICATION");
			}
		}

        public async Task<Guid> VendorAssign(Guid catid)
        {
            try
            {
                // Step 1: Get all vendor ids related to the given category
                var vendorInCategory = await _context.VendorCategories
                    .Where(c => c.CategoryId == catid)
                    .Include(c => c.Vendor)
                        .ThenInclude(v => v.Pickups)
                            .ThenInclude(p => p.Statuss)
                    .ToListAsync();

                if (vendorInCategory == null || !vendorInCategory.Any())
                {
                    throw new Exception("No vendors found for the given category.");
                }

                // Step 2: Get the vendor ids
                var vendorIds = vendorInCategory.Select(vc => vc.Vendor.Id).ToList();

                // Step 3: Get all pickups associated with the vendors in this category
                var pickupsForVendors = await _context.Pickups
                    .Where(p => vendorIds.Contains(p.VendorId))
                    .Include(p => p.Statuss) // Include pickup statuses
                    .ToListAsync();

                // Step 4: Filter pickups that are 'Pending'
                var pendingPickups = pickupsForVendors
                    .Where(p => p.Statuss.Any(s => s.Statuss == "Pending"))
                    .ToList();

                // Step 5: Calculate completion rate for each vendor
                var vendorCompletionRates = vendorIds.Select(vendorId =>
                {
                    var totalPickups = pickupsForVendors.Count(p => p.VendorId == vendorId);
                    var completedPickups = pickupsForVendors.Count(p => p.VendorId == vendorId && p.Statuss.Any(s => s.Statuss == "Completed"));
                    var completionRate = totalPickups == 0 ? 100 : (completedPickups / (double)totalPickups) * 100;

                    return new
                    {
                        VendorId = vendorId,
                        CompletionRate = completionRate,
                        PendingWorks = pendingPickups.Count(p => p.VendorId == vendorId)
                    };
                }).ToList();

                // Step 6: Sort vendors by completion rate
                var sortedVendors = vendorCompletionRates
                    .OrderByDescending(v => v.CompletionRate)
                    .ThenBy(v => v.PendingWorks)
                    .ToList();

                // Step 7: Assign vendor if there's no pickup data
                if (!sortedVendors.Any())
                {
                    // Return the first available vendor from the category
                    return vendorInCategory.First().Vendor.Id;
                }

                // Step 8: Return the vendor id with the highest priority
                var selectedVendor = sortedVendors.First();
                return selectedVendor.VendorId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex); // Preserve stack trace for better debugging
            }
        }

        public async Task<bool> CategoryAddvendor(Guid id, Guid catid)
        {
            try
            {
                var exist = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
                if (exist == null)
                {
                    return false;
                }

                var catexist = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == catid);
                if (catexist == null)
                {
                    return false;
                }
                var cat_in_vandorcat = await _context.VendorCategories.Include(r => r.Vendor).FirstOrDefaultAsync(v => v.VendorId == id && v.CategoryId == catid);
                if (cat_in_vandorcat != null)
                {
                    return false;
                }


                var category = new VendorCategoryAddDto
                {
                    VendorId = id,
                    CategoryId = catid
                };
                var cat = _mapper.Map<VendorCategory>(category);
                _context.VendorCategories.Add(cat);
                await _context.SaveChangesAsync();
                return true;


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the category for the vendor: {ex.Message}", ex);
            }
        }


        public async Task<VendorOnline> VendorActivity(Guid id)
        {
            try
            {
                var data = await _dbAccess.GetAllVendorOnline();
                var vendor = data.FirstOrDefault(p => p.VendorId == id);
                var vendorAddress= await _dbAccess.GetAllVendorAddresses();
                var adrs=vendorAddress.FirstOrDefault(p => p.VendorId == id);

                if (vendor == null)
                {
                    var latlong = GetCoordinatesFromAddress(adrs.City, adrs.PostalCode);
                    var vonline= new VendorOnline
                    {
                        IsActive=true,
                        VendorId=id,
                        Lat=latlong.Result.lat,
                        Long=latlong.Result.lon
                    };
                    await _context.VendorOnline.AddAsync(vonline);
                    await _context.SaveChangesAsync();
                    return vonline;
                }

                return new VendorOnline();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the category for the vendor: {ex.Message}", ex);
            }
        }




        private async Task<(decimal lat, decimal lon)> GetCoordinatesFromAddress(string city, string postalCode)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0");

                var address = GetFullAddress(city, postalCode);
                var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json";

                Console.WriteLine($"Requesting: {url}");

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to fetch coordinates. Error: {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<GeoResponse>>(json);

                if (data == null || !data.Any())
                {
                    throw new Exception($"No coordinates found for the address: {address}");
                }

                decimal lat = Convert.ToDecimal(data[0].Lat, CultureInfo.InvariantCulture);
                decimal lon = Convert.ToDecimal(data[0].Lon, CultureInfo.InvariantCulture);

                return (lat, lon);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in fetching coordinates: {ex.Message}", ex);
            }
        }

        private string GetFullAddress(string city, string postalCode)
        {
            return $"{city}, {postalCode}";
        }

        public class GeoResponse
        {
            [JsonPropertyName("lat")]
            public string Lat { get; set; }

            [JsonPropertyName("lon")]
            public string Lon { get; set; }
        }
        public async Task<Guid> GetNearestVendorPerson(Guid id)
        {
            var user = await _dbAccess.GetAllAddressAddresses();
            var userAddress = user.FirstOrDefault(p => p.Id == id);
            var (UserLat, UserLon) = await _deliveryService.GetCoordinatesFromAddress(userAddress);
            var Persons = await _dbAccess.GetAllVendorOnline();
            var ActivepPersons=Persons.Where(p=>p.IsActive==true).ToList();
            if (ActivepPersons == null)
            {
                throw new Exception("No active delivery persons available.");

            }
            VendorOnline nearestVendorPerson = null;
            double shortestDistance = double.MaxValue;
            foreach (var dp in ActivepPersons)
            {
                double distance = CalculateDistance(
                    UserLat, UserLon,
                    dp.Lat, dp.Long
                );

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestVendorPerson = dp;
                }
            }

            return nearestVendorPerson.VendorId;
        }
        private double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const double R = 6371; // Radius of Earth in kilometers
            double dLat = ToRadians((double)(lat2 - lat1));
            double dLon = ToRadians((double)(lon2 - lon1));

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians((double)lat1)) * Math.Cos(ToRadians((double)lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c; // Distance in kilometers
        }

        private double ToRadians(double angle) => Math.PI * angle / 180.0;

    }
}
