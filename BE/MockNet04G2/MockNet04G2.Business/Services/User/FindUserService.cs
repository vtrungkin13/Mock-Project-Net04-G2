using MockNet04G2.Business.Services.Interfaces;
using MockNet04G2.Core.Repositories.Interfaces;

namespace MockNet04G2.Business.Services.User
{
    public class FindUserService 
    {
        private readonly IUserRepository _userRepository;

        public FindUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Core.Models.User> ExecuteAsync(string name)
        {
            return await _userRepository.FindUserByNameAsync(name);
        }

        public async Task<Core.Models.User> ExecuteAsync(int id)
        {
            return await _userRepository.FindUserByIdAsync(id);
        }
    }
}
