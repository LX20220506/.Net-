using System;
using System.Collections.Generic;
using System.Text;
using Models;
using MyBook.IRepository;

namespace MyBook.Repository
{
    /// <summary>
    /// IBookInfoRepository接口繼承了IBaseRepository接口
    /// BookInfoRepository類繼承了IBookInfoRepository接口,
    /// 所以BookInfoRepository類要實現IBaseRepository接口，
    /// 因為BaseRepository類實現繼承了IBaseRepository接口， 并實現了IBaseRepository接口，
    /// 所以BookInfoRepository繼承BaseRepository就相當於實現了IBaseRepository接口
    /// </summary>
    public class BookInfoRepository:BaseRepository<BookInfo>,IBookInfoRepository
    {
    }
}
