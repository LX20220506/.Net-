using System;
using System.Collections.Generic;
using System.Text;
using Models;
using MyBook.IRepository;
using MyBook.IService;


namespace MyBook.Service
{
    public class TypesInfoService:BaseService<TypesInfo>,ITypesInfoService
    {
        private readonly ITypesInfoRepository _iTypesInfoRepository;
        public TypesInfoService(ITypesInfoRepository iTypesInfoRepository) {
            base._iBaseRepository = iTypesInfoRepository;
            this._iTypesInfoRepository = iTypesInfoRepository;
        }
    }
}
