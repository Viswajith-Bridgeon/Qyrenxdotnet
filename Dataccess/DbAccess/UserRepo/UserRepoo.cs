using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.UserRepo
{
    public class UserRepoo:IuserRepo
    {
        private readonly QyrenxContext _context;
        
        public UserRepoo(QyrenxContext qyrenxContext)
        {
            _context = qyrenxContext;
        }
     




   


        public async Task<List<User>> GetUsers()
        {
            try
            {
                var data = await _context.Users.ToListAsync();
                var u = data.Where(e => e.Role != "Admin").ToList();
                return u;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(p=>p.Id==id); 
                if (user == null)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }




        public async Task<string> BlockOrUnblock(Guid id)
        {
            try
            {
                var data = await _context.Users.ToListAsync();
                var us = data.FirstOrDefault(p => p.Id == id);
                if (us == null)
                {
                    return  "user is not found";
                }
                if (us.IsBlock)
                {
                    us.IsBlock = false;
                    await _context.SaveChangesAsync();
                    return "user is blocked";
                }
                    us.IsBlock = !us.IsBlock;
                    await _context.SaveChangesAsync();
                    return "user is unblocked";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }





        public async Task<List<User>> SearchUsers(string name)
        {
            try
            {
                var data = await _context.Users.ToListAsync();
                var users = data.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var data=await _context.Users.FirstOrDefaultAsync(p=>p.Email==email);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
