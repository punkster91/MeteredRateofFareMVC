using MeteredRateofFare.Models;

namespace MeteredRateofFare.Managers
{
    public class FareManager
    {

        //$.35 for each additional unit
        //Apply base charges
        //Apply additional charges - minutes/miles
        //Apply night surchage
        //Apply peakhour surcharges
        //Apply taxes
        private decimal _entryCharge = 3.00M;

        private decimal _unitCharge = 0.35M;

        private decimal _nightCharge = 0.50M;

        private decimal _peakCharge = 1.00M;

        private decimal _tax = 0.50M;

        private decimal GetMilesSurcharge(int milesTraveled)
        {
            return milesTraveled * 5 * _unitCharge;
        }

        private decimal GetMinutesSurcharge(int minutesTraveled)
        {
            return minutesTraveled * _unitCharge;
        }

        private decimal GetNightSurchage(TimeOnly startTime)
        {
            var eightPM = new TimeOnly(20, 0, 0, 0);
            var sixAM = new TimeOnly(6, 0, 0, 0);

            return startTime.IsBetween(eightPM, sixAM) ? _nightCharge : 0M;
        }

        private decimal GetPeakSurcharge(DateOnly startDate, TimeOnly startTime)
        {
            var fourPM = new TimeOnly(16, 0, 0, 0);
            var eightPM = new TimeOnly(20, 0, 0, 0);

            if ((startDate.DayOfWeek == DayOfWeek.Monday
                || startDate.DayOfWeek == DayOfWeek.Tuesday
                || startDate.DayOfWeek == DayOfWeek.Wednesday
                || startDate.DayOfWeek == DayOfWeek.Thursday
                || startDate.DayOfWeek == DayOfWeek.Friday) 
                && startTime.IsBetween(fourPM, eightPM))
            {
                return _peakCharge;
            }
            return 0M;            
        }

        private decimal GetStateTax()
        {
            return _tax;
        }

        public decimal ComputeTotalCost(MeteredFareModel fare)
        {
            var total = _entryCharge;

            total += GetMilesSurcharge(fare.MilesTraveled);
            total += GetMinutesSurcharge(fare.MinutesTraveled);
            total += GetNightSurchage(fare.StartTime);
            total += GetPeakSurcharge(fare.StartDate, fare.StartTime);
            total += GetStateTax();

            fare.TotalPrice = total;
            return fare.TotalPrice;
        }
    }
}
