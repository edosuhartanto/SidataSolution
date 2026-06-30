using Sidata.Abstractions.Interfaces;
using Sidata.SLIP2.Data.Enums;

namespace Sidata.SLIP2.Data.DTOs.Definitions
{
    /// <summary>
    /// Dto for InstrumentDefinition entity class
    /// </summary>
    public class InstrumentDefinitionDto : IMasterClass
    {
        public long Id { get; set; }

        /// <summary>
        /// Foreign Key yg menghubungkan pelanggan ke 
        /// merchant tempat pelanggan ini bernaung (dimiliki).        /// 
        /// </summary>
        
        #region FK (Merchant, InstrumentType)
        /// <summary>
        /// id of the merchant who owns this definition
        /// </summary>
        public long MerchantId { get; set; }

        /// <summary>
        /// id of the instrument type for this definition
        /// type of VOUCHER, POINT, MEMBERSHIP, etc
        /// a departement
        /// </summary>
        public long InstrumentTypeId { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// code of the instrument definition.
        /// user-defined, user-friendly code
        /// </summary>
        public string DefinitionCode { get; set; } = default!;

        /// <summary>
        /// name of the instrument definition.
        /// </summary>
        public string DefinitionName { get; set; } = default!;

        /// <summary>
        /// type of balance calculation strategy
        /// 0=undefined, 1=reusable, 2=bucket, 3=onetime / single use
        /// </summary>
        public BalanceStrategyType BalanceStrategyType { get; set; }

        #region Earn Behavior
        /// <summary>
        /// how much value the user need to have to earn 1 unit
        /// ex.100.000idr gets 100points = 0.001
        /// 1 idr get 1000 points = 1000;
        /// 1 idr get 1 point = 1;
        /// </summary>
        public decimal EarnConversionRate { get; set; }

        /// <summary>
        /// rounding mode for earn conversion
        /// 0=none, 1=floor, 2=ceiling, 3=round
        /// </summary>
        public RoundingMode EarnRoundingMode { get; set; }

        /// <summary>
        /// value to be used as a factor for rounding the earning
        /// after calculate with convertion rate
        /// ex. factor=500, roundingmode=floor, earning=1560 => 1500;
        /// factor=500, roundingmode=ceiling, earning=1560 => 2000;
        /// </summary>
        public decimal EarnRoundingFactor { get; set; }
        #endregion

        #region Redeem Behavior
        /// <summary>
        /// how much value user can get when redeem 1 unit
        /// ex. 25 points gets 1 idr = 0.04;
        /// 1 points get 100 idr = 100;
        /// 1 points get 1 idr = 1;
        /// </summary>
        public decimal RedeemConversionRate { get; set; }

        /// <summary>
        /// rounding mode for redeem conversion
        /// 0=none, 1=floor, 2=ceiling, 3=round
        /// </summary>
        public RoundingMode RedeemRoundingMode { get; set; }

        /// <summary>
        /// value to be used as a factor for rounding the redeeming value
        /// after calculate with convertion rate
        /// ex. factor=500, roundingmode=floor, redeem=1560 => 1500;
        /// factor=500, roundingmode=ceiling, redeem=1560 => 2000;
        /// </summary>
        public decimal RedeemRoundingFactor { get; set; }
        #endregion

        /// <summary>
        /// definition is still active
        /// </summary>
        public bool IsActive { get; set; }
        #endregion

        #region Selectable Behavior
        public bool AllowTopup { get; set; }

        public bool AllowDebit { get; set; }

        public bool HasExpiration { get; set; }

        public int ExpireAfterDays { get; set; }

        public decimal MaximumBalance { get; set; }

        public bool SingleUseOnly { get; set; }

        public bool Transferable { get; set; }

        public bool AllowNegativeBalance { get; set; }
        #endregion
    }
}
