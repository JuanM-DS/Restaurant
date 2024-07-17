using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Application.DTOs.Entities.ApplicationUser;
using Restaurant.Core.Application.Interfaces.Repositories;
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

        public async Task<bool> Delete(ApplicationUserDto userDto)
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

        public IEnumerable<ApplicationUserDto> GetAll()
        {
            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDto>>(_users.AsEnumerable());
            return userDTos;
        }

        public async Task<ApplicationUserDto?> GetById(string id)
        {
            var user = await _users.FindAsync(id);
            var userDTo = _mapper.Map<ApplicationUserDto>(user);
            return userDTo;
        }

        public IEnumerable<ApplicationUserDto> GetWithInclude(List<string> properties)
        {
            IQueryable<ApplicationUser> query = _users;

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDto>>(query.AsEnumerable());
            return userDTos;
        }

        public async Task<ApplicationUserDto?> GetWithInclude(string id, List<string> properties)
        {
            IQueryable<ApplicationUser> query = _users;

            foreach (var item in properties)
            {
                query = query.Include(item);
            }

            var user = await query.FirstOrDefaultAsync(x => x.Id == id);

            var userDTo = _mapper.Map<ApplicationUserDto>(user);

            return userDTo;
        }

        public IEnumerable<ApplicationUserDto> GetWithInclude(UserQueryFilters filters, List<string> properties)
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

            var userDTos = _mapper.Map<IEnumerable<ApplicationUserDto>>(query.AsEnumerable());
            return userDTos;
        }

        public async Task<bool> Update(string id, ApplicationUserDto userDto)
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
