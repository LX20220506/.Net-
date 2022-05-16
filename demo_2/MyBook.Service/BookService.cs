using IBaseService;
using Models;
using MyBook.IService;
using System;
using System.Collections.Generic;
using System.Text;
using MyBook.IRepository;

namespace MyBook.Service
{
    public class BookService:BaseService<BookInfo>,IBookService
    {
        private readonly IBookInfoRepository _iBookInfoRepository;
        /// <summary>
        /// Repository文件夾下主要是做數據訪問的(相當於ADL)
        /// Service文件夾下主要的是做業務處理的(相當于BLL)
        /// </summary>
        /// <param name="iBookService">相當於數據庫Book表的上下文對象</param>
        public BookService(IBookInfoRepository iBookInfoRepository) {
            base._iBaseRepository = iBookInfoRepository;
            _iBookInfoRepository= iBookInfoRepository;
        }
    }
}
