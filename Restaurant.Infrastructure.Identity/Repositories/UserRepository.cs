using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.DTOs.Identity.EntityDTOs;
using Restaurant.Core.Application.Interfaces.Persistence.Repositories;
using Restaurant.Core.Application.QueryFilters;
using Restaurant.Infrastructure.Identity.Context;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Repositories
{
    public class UserRepository(RestaurantIdentityDbContext context, IMapper mapper) : IUserRepository
    {
        private readonly RestaurantIdentityDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly DbSet<ApplicationUser> _users = context.Users;

        public async Task<bool> Delete(ApplicationUserDTO userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            
            try
            {
                _users.Remove(user);

                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ApplicationUserDTO> GetAll()
        {
            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDTO>>(_users.AsEnumerable());
            return userDTos;
        }

        public async Task<ApplicationUserDTO?> GetById(string id)
        {
            var user = await _users.FindAsync(id);
            var userDTo = _mapper.Map<ApplicationUserDTO>(user);
            return userDTo;
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(List<string> properties)
        {
            IQueryable<ApplicationUser> query = _users;

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
            return userDTos;
        }

        public async Task<ApplicationUserDTO?> GetWithInclude(string id, List<string> properties)
        {
            IQueryable<ApplicationUser> query = _users;

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            var user = await query.FirstOrDefaultAsync(x => x.Id == id);

            var userDTo = _mapper.Map<ApplicationUserDTO>(user);

            return userDTo;
        }

        public IEnumerable<ApplicationUserDTO> GetWithInclude(UserQueryFilters filters, List<string> properties)
        {
            IQueryable<ApplicationUser> query = _users;

            if (filters.FirstName is not null)
                query = query.Where(x => x.FirstName == filters.FirstName);

            if(filters.LastName is not null)
                query = query.Where(x => x.LastName == filters.LastName);

            if (filters.UserName is not null)
                query = query.Where(x => x.UserName == filters.UserName);

            if (filters.Email is not null)
                query = query.Where(x => x.Email == filters.Email);

            if (filters.EmailConfirmed is not null)
                query = query.Where(x => x.EmailConfirmed == filters.EmailConfirmed);


            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDTO>>(query.AsEnumerable());
            return userDTos;
        }

        public async Task<bool> Update(string id, ApplicationUserDTO userDto)
        {
            var user = await _users.FindAsync(id);

            _mapper.Map(userDto,  user);

            try
            {
                _users.Update(user);

                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
