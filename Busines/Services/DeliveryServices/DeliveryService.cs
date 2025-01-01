

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Dataccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.Models.DTOs.Deliverypersons;
using Qyrenx.Business.Services.CloudinaryService;
using Microsoft.AspNetCore.Http;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Business.DTOs.Deliverypersons;

namespace Qyrenx.Business.Services.DeliveryServices
{
    public class DeliveryService: IDeliveryService
    {
        private readonly QyrenxContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _autoMapping;
        private readonly IJwtService _jwtServices;
        private readonly ILogger<DeliveryService> _logger;
        private readonly ICloudinaryService _cloudinaryService;
        public DeliveryService(QyrenxContext context, IConfiguration configuration, IMapper autoMapping,IJwtService jwtService,ILogger<DeliveryService> logger,ICloudinaryService cloudinaryService)
        {
            _autoMapping = autoMapping;
            _context = context; 
            _configuration = configuration;
            _jwtServices = jwtService;  
            _logger = logger;
            _cloudinaryService = cloudinaryService; 
            
        }
        public async Task <bool> Register(DeliveryPersonRegDto dto,IFormFile license)
        {
            try
            {
                var exist =  await _context.DeliveryPersons.FirstOrDefaultAsync(p => p.Email == dto.Email);
                if (exist != null)
                {
                    return false;
                }
                var lisemce = await _cloudinaryService.UploadDocumentAsync(license);
                var mapdata = new DeliveryPerson
                {
                    Name=dto.DeliveryPersonName,
                    Email=dto.Email,
                    DrivingLicense= lisemce,
                    HashPassword= BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Mobile=dto.Mobile
                };
                _context.DeliveryPersons.Add(mapdata);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.InnerException.Message);
            }
            
        }
        public async Task<DeliveryPersonLoginViewDto>Login(DeliveryPersonLoginDto dto)
        {
            try
            {
                
                _logger.LogInformation($"Searching for delivery person with email: {dto.Email}");

                var exist = await _context.DeliveryPersons.SingleOrDefaultAsync(p => p.Email == dto.Email );
                if (exist != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(dto.Password, exist.HashPassword))
                    {
                        if (exist.IsVerified == true)
                        {
                            if (exist.IsBlock == false)
                            {
                                var token = _jwtServices.GenerateJwt(exist.Id, exist.Email, exist.Role);
                                return new DeliveryPersonLoginViewDto { DeliveryPersonName = exist.Name, id = exist.Id, token = token };

                            }
                            return new DeliveryPersonLoginViewDto { Error = "person is blocked " };
                        }
                        return new DeliveryPersonLoginViewDto { Error = "persons verification is pending!" };
                    }
                    return new DeliveryPersonLoginViewDto { Error = "enter valid credentials" };
                }
                return new DeliveryPersonLoginViewDto { Error = "no such delivery person is registered" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while querying for email {dto.Email}: {ex.Message}");
                throw new Exception(ex.InnerException.Message);
            }

        }

        public async Task<bool> verify(string mail)
        {
            try
            {
                var verification=await _context.DeliveryPersons.FirstOrDefaultAsync(p=>p.Email.Equals(mail));
                if(verification != null)
                {
                    verification.IsVerified = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            } 
        }


        public async Task<IEnumerable<DeliveryPersonDto>> GetAllDeliveryPeresons()
        {
            try
            {
                var deliverypersons =  _context.DeliveryPersons;
                if(deliverypersons != null)
                {
                    return deliverypersons.Select(p => new DeliveryPersonDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        IsBlock = p.IsBlock,
                        IsVerified = p.IsVerified,
                        Email = p.Email,
                        Mobile = p.Mobile,
                        DrivingLicense = Path.ChangeExtension(p.DrivingLicense,".jpg"),
                        Role= p.Role
                    }).ToList();
                }
                return Enumerable.Empty<DeliveryPersonDto>();  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async  Task<DeliveryPersonDto> GetDeliveryPeresonById(Guid id)
        {
            try
            {
                var deliveryperson= _context.DeliveryPersons.FirstOrDefault(p=>p.Id==id);
                if(deliveryperson != null)
                {
                    return new DeliveryPersonDto
                    {
                        Id=deliveryperson.Id,
                        Name = deliveryperson.Name, 
                        IsBlock = deliveryperson.IsBlock,
                        IsVerified = deliveryperson.IsVerified,                      
                        Email = deliveryperson.Email,   
                        Mobile = deliveryperson.Mobile, 
                        DrivingLicense= deliveryperson.DrivingLicense,
                        Role = deliveryperson.Role
                       
                    };
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task <bool>BlockOrUnblock(Guid id)
        {
            try
            {
                var exist =  _context.DeliveryPersons.FirstOrDefault(p=>p.Id==id);
                if (exist != null)
                {
                    exist.IsBlock = !exist.IsBlock;
                    _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.InnerException.Message); 
            }
           
        }
        public async Task <DeliveryPersonOnline>DeliveryPersonActivity(Guid id,decimal latt,decimal lonn)
        {
            try
            {
                var user = await _context.DeliveryPersons.FirstOrDefaultAsync(p => p.Id == id);
                var useronline = await _context.DeliveryPersonOnlines.FirstOrDefaultAsync(p => p.DeliveryPersonId == user.Id);
                if (useronline != null)
                {
                    useronline.IsActive = false;
                    await _context.SaveChangesAsync();
                }
                else if (useronline == null)
                {
                    var usernew = new DeliveryPersonOnline
                    {
                        DeliveryPersonId = user.Id,
                        IsActive = false,
                        Lat = latt,
                        Long = lonn,
                    };
                    return usernew;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<DeliveryPersonOnlineDto>> GetAllDeliveryPersonOnline()
        {
            try
            {
                var user = _context.DeliveryPersonOnlines;
                if(user != null)
                {
                    return user.Select(p => new DeliveryPersonOnlineDto
                    {
                        DeliveryPersonId= p.Id,
                        IsActive = p.IsActive,
                        Lat = p.Lat,
                        Long = p.Long
                    }).ToList();
                }
                return new List<DeliveryPersonOnlineDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<DeliveryPersonOnlineDto>> GetActiveDeliveryPersons()
        {
            var user = await _context.DeliveryPersonOnlines.Where(p=>p.IsActive==true).ToListAsync();
            if(user != null)
            {
                return user.Select(p=>new DeliveryPersonOnlineDto
                {
                    DeliveryPersonId = p.Id,
                    IsActive = p.IsActive,
                    Lat = p.Lat,
                    Long = p.Long
                }).ToList();
            }
            return new List<DeliveryPersonOnlineDto>();
        }




    }
}
       
