using Microsoft.AspNetCore.Mvc;

namespace cp4.Controllers
{
    public interface IExchangeController
    {
        JsonResult GetExchangeRate();
    }
}

public class ConversionRate
{
    public double BRL { get; set; }
}