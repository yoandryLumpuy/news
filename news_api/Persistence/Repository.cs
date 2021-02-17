
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using news_api.Core.Model;
using news_api.DataTransferObjects;
using news_api.Core;
using AutoMapper;
using news_api.Extensions;
using news_api.Persistence;

namespace Reservation_API.Persistence
{
    public class Repository : IRepository
    {
        private readonly NewsDbContext _newsDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Repository(NewsDbContext newsDbContext, UserManager<User> userManager,
                  IUnitOfWork unitOfWork, IMapper mapper)
        {
            _newsDbContext = newsDbContext;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }  

        public async Task<User> GetUserAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }         
    }
}
