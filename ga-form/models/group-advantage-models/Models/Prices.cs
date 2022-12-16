namespace Gmsca.Group.GA.Models;

public class Prices
{
    public float classPremium { get; set; }
    public float healthAndDentalPremium { get; set; }
    public float assumptionLifePremium { get; set; }
    public EmployeeTypesPrices dental { get; set; } = new();
    public EmployeeTypesPrices health { get; set; } = new();
    public Price accidentalDeathAndDismemberment { get; set; } = new();
    public Price criticalIllness { get; set; } = new();
    public Price dependantCriticalIllness { get; set; } = new();
    public Price dependantLifeInsurance { get; set; } = new();
    public Price lifeInsurance { get; set; } = new();
    public Price longTermDisability { get; set; } = new();
    public Price secondMedicalOpinion { get; set; } = new();
    public Price shortTermDisability { get; set; } = new();
}

public class EmployeeTypesPrices
{
    public Price couple { get; set; } = new();
    public Price family { get; set; } = new();
    public Price single { get; set; } = new();
}

public class Price
{
    public Price(long volume, float rate)
    {
        this.volume = volume;
        this.rate = rate;
        total = rate * volume;
    }
    public Price(long volume, float rate, float total)
    {
        this.volume = volume;
        this.rate = rate;
        this.total = total;
    }
    public Price() { }

    public float rate { get; set; }
    public float total { get; set; }
    public long volume { get; set; }
}

