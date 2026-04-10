using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models;

public class CompanyInformation
{
    public int Id { set; get; }
    [DisplayName("Company Name")]
    public string Name { set; get; }
    [DisplayName("Address")]

    public string Address { set; get; }
    [DisplayName("Phone Number")]
    public int PhoneNo { set; get; }

    [DisplayName("N.S.S.F.No")]
    public string NSSFNO { set; get; }
    [DisplayName("N.H.I.F.NO")]
    public string NHIFNO { set; get; }
    [DisplayName("K.R.A Pin")]
    public string KRAPIN { set; get; }
    [DisplayName("Contact Person")]
    public string ContactPerson { set; get; }
    [DisplayName("Logo")]
    public string Logo { set; get; }
    [DisplayName("Postal Code")]
    public string PostalCode { set; get; }
    [DisplayName("City Name")]
    public int CityId { set; get; }
    public City City { set; get; }
    [DisplayName("Country Name")]
    public int CountryId { set; get; }
    public Country Country { set; get; }



}

