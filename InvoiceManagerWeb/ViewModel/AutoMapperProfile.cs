// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using AutoMapper;

using DAL.Models;
using InvoiceManagerWeb.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InvoiceManager.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<DeliveryRoute, DeliveryRouteViewModel>().ReverseMap();
            CreateMap<BillingInformation, BillingInformationViewModel>().ReverseMap();
            CreateMap<Store, StoreViewModel>().ReverseMap();
        }
    }
}
