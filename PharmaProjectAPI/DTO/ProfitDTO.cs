﻿namespace PharmaProjectAPI.DTO
{
    public class ProfitDTO
    {
        public decimal TotalSales { get; set; }
        public decimal TotalCostOfGoods { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalSales - TotalCostOfGoods - TotalExpenses;
    }
}
