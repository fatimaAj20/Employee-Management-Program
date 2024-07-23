using Courseproject.Common.Dtos.Address;

namespace Courseproject.Common.Interfaces;

public interface IAddressService
{
    public Task<int> CreateAddressAsync(AddressCreate addressCreate);
    public Task UpdateAddressAsync(AddressUpdate addressUpdate);
    public Task DeleteAddressAsync(AddressDelete addressDelete);
    public Task<AddressGet> GetAddressAsync(int id);
    public Task<List<AddressGet>> GetAddressesAsync();

}
