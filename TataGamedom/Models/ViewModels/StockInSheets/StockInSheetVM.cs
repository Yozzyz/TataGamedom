using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TataGamedom.Models.Dtos.StockInSheets;

namespace TataGamedom.Models.ViewModels.StockInSheets
{
    public class StockInSheetVM
    {
        public int Id { get; set; }

        [Display(Name = "進貨狀態")]
        public int StockInStatusId { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "供應商")]
        public int SupplierId { get; set; }

        [Display(Name = "數量")]
        [Required(ErrorMessage = "{0} 必填")]
        [Range(1, 99999999, ErrorMessage = "{0}不得小於1")]
        public int Quantity { get; set; }

        [Display(Name = "下單日")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} 必填")]
        public DateTime OrderRequestDate { get; set; }

        [Display(Name = "到貨日")]
        [DataType(DataType.Date)]
        public DateTime? ArrivedAt { get; set; }
    }
    public static class StockInSheetExts
    {
        public static StockInSheetDto ToDto(this StockInSheetVM vm)
        {
            return new StockInSheetDto
            {
                Id = vm.Id,
                StockInStatusId = vm.StockInStatusId,
                SupplierId = vm.SupplierId,
                Quantity = vm.Quantity,
                OrderRequestDate = vm.OrderRequestDate,
                ArrivedAt = vm.ArrivedAt
            };
        }
    }
}