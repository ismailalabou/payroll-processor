using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace PayrollProcessor.Functions.Features.Payrolls
{
    public class PayrollEntity : TableEntity
    {
        public DateTimeOffset CheckDate { get; set; }
        public Guid EmployeeId { get; set; }
        public double GrossPayroll { get; set; }
        public string PayrollPeriod { get; set; } = "";

        public static class Map
        {
            public static Payroll To(PayrollEntity entity)
            {
                return new Payroll(Guid.Parse(entity.RowKey))
                {
                    CheckDate = entity.CheckDate,
                    EmployeeId = entity.EmployeeId,
                    GrossPayroll = entity.GrossPayroll,
                    PayrollPeriod = entity.PayrollPeriod,
                };
            }

            public static PayrollEntity From(Payroll payroll)
            {
                return new PayrollEntity
                {
                    PartitionKey = "Payroll",
                    RowKey = payroll.Id.ToString("n"),
                    CheckDate = payroll.CheckDate,
                    EmployeeId = payroll.EmployeeId,
                    GrossPayroll = payroll.GrossPayroll,
                    PayrollPeriod = payroll.PayrollPeriod
                };
            }
        }
    }
}
