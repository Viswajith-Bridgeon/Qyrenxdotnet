using AutoMapper;
using Qyrenx.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Mapper;
using Qyrenx.Models.DTOs.Deliverypersons;
using Qyrenx.Models.Entities;
using Qyrenx.Services.JwtServices;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Qyrenx.Services.EmailServices;

namespace Qyrenx.Services.DeliveryServices
{
    public class DeliveryService:IDeliveryService
    {
        private readonly QyrenxContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _autoMapping;
        private readonly IJwtService _jwtServices;
        private readonly ILogger<DeliveryService> _logger;
        private readonly IEmailServices _emailServices;
        public DeliveryService(QyrenxContext context, IConfiguration configuration, IMapper autoMapping,IJwtService jwtService,ILogger<DeliveryService> logger,IEmailServices emailServices)
        {
            _autoMapping = autoMapping;
            _context = context; 
            _configuration = configuration;
            _jwtServices = jwtService;  
            _logger = logger;
            _emailServices = emailServices; 
        }
        public async Task <bool> Register(DeliveryPersonRegDto dto)
        {
            try
            {
                var exist = await _context.DeliveryPersons.FirstOrDefaultAsync(p => p.Email == dto.Email);
                if (exist != null)
                {
                    return false;
                }
                var mapdata = new DeliveryPerson
                {
                    Name=dto.DeliveryPersonName,
                    Email=dto.Email,
                    DeliveryLocationZipcode=dto.DeliveryLocationZipcode,
                    DrivingLicense=dto.DrivingLicense,
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
                        if (exist.IsVerified == false)
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
                        Date = p.Date,
                        DeliveryLocationZipcode = p.DeliveryLocationZipcode,
                        Email = p.Email,
                        Mobile = p.Mobile,
                        DrivingLicense = p.DrivingLicense,
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
                var deliveryperson=await _context.DeliveryPersons.FirstOrDefaultAsync(p=>p.Id==id);
                if(deliveryperson != null)
                {
                    return new DeliveryPersonDto
                    {
                        Id=deliveryperson.Id,
                        Name = deliveryperson.Name, 
                        IsBlock = deliveryperson.IsBlock,
                        IsVerified = deliveryperson.IsVerified,
                        Date = deliveryperson.Date,
                        DeliveryLocationZipcode= deliveryperson.DeliveryLocationZipcode,
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
        public async Task<bool> SendOtp(string email)
        {
            var issend=await _emailServices.sendOtp(email);
            if (issend)
            {
                return true;
            }
            return false;
        }

        public async Task <bool> VerifyOtp(string email,string otp)
        {
            var isverified = _emailServices.verifyOtp(email, otp);
            if (isverified) 
            {
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }    

    }
}
       
