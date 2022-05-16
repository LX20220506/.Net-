using IBaseService;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBook.IService
{
    public interface IBookService: IBaseService<BookInfo>
    {
    }
}
