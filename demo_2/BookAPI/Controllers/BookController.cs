using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBook.IRepository;
using MyBook.Repository;
using Result;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInfoRepository _iBookInfoRepository;
        public BookController(IBookInfoRepository iBookInfoRepository) {
            _iBookInfoRepository = iBookInfoRepository;
        }

        [HttpGet]
        public async Task<Result.Result> GetAllBookInfo() {
            var data = await _iBookInfoRepository.QueryAsync();
            if (data == null) return Result.ResultHelper.Error("暫時沒有書籍");
            return ResultHelper.Success(data);
        } 
    }
}
