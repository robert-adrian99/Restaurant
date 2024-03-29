﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Helps
{
    class Constants
    {
        static public double MenuDiscount
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("menuDiscount"));
            }
        }

        static public double DeliveryCost
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("deliveryCost"));
            }
        }

        static public double OrderDiscount
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("orderDiscount"));
            }
        }

        static public int DiscountTime
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings.Get("discountTime"));
            }
        }

        static public double OrderPriceGreaterThanForDelivery
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("orderPriceGreaterThanForDelivery"));
            }
        }

        static public double NumberMinOrders
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("numberMinOrders"));
            }
        }

        static public double OrderPriceGreaterThanForDiscount
        {
            get
            {
                return double.Parse(ConfigurationManager.AppSettings.Get("orderPriceGreaterThanForDiscount"));
            }
        }

        static public int DeliveryTime 
        { 
            get
            {
                return int.Parse(ConfigurationManager.AppSettings.Get("deliveryTime"));
            }
        }

        static public int MinTotalQuantity
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings.Get("minTotalQuantity"));
            }
        }
    }
}
