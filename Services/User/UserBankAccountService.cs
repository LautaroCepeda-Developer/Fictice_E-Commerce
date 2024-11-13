using AutoMapper;
using DTOs.User;
using Models;
using Models.Context;
using Repository.Interfaces;
using Services.Interfaces;

namespace Services.User
{
    public class UserBankAccountService(IRepository<UserBankAccount> userBankAccountRepository, IMapper mapper, DatabaseContext context) : ICommonService<UserBankAccountDTO, UserBankAccountInsertDTO, UserBankAccountUpdateDTO>
    {
        private readonly IRepository<UserBankAccount> _repository = userBankAccountRepository;
        private readonly IMapper _mapper = mapper;
        private readonly DatabaseContext _databaseContext = context;

        public List<string> Errors { get; } = [];

        public async Task<IEnumerable<UserBankAccountDTO>> Get()
        {
            var bankAccount = await _repository.Get();

            return bankAccount.Select(_mapper.Map<UserBankAccountDTO>);
        }


        public async Task<UserBankAccountDTO?> GetById(int id)
        {
            var bankAccount = await _repository.GetById(id);

            if (bankAccount is null) return null;

            var bankAccountDTO = _mapper.Map<UserBankAccountDTO>(bankAccount);

            return bankAccountDTO;
        }

        public async Task<UserBankAccountDTO> Add(UserBankAccountInsertDTO userBankAccountInsertDTO)
        {
            // Automapping the DTO to the User
            var bankAccount = _mapper.Map<UserBankAccount>(userBankAccountInsertDTO);

            // Adding the user to the DB and saving the changes
            await _repository.Add(bankAccount);
            await _repository.Save();

            // Mapping the DTO to be returned
            var bankAccountDTO = _mapper.Map<UserBankAccountDTO>(bankAccount);

            // Returning the added user
            return bankAccountDTO;

        }

        public async Task<UserBankAccountDTO?> Update(int id, UserBankAccountUpdateDTO updateDTO)
        {
            // Getting the user
            var bankAccount = await _repository.GetById(id);

            // Checking if the user exists
            if (bankAccount is null) return null;

            // Mapping the entity
            bankAccount = _mapper.Map(updateDTO, bankAccount);

            // Updating the user and saving the changes
            _repository.Update(bankAccount);
            await _repository.Save();

            // Mapping the DTO to be returned
            var bankAccountDTO = _mapper.Map<UserBankAccountDTO>(bankAccount);

            // Returning the updated user
            return bankAccountDTO;
        }

        public async Task<UserBankAccountDTO?> Delete(int id)
        {
            // Getting the user
            var bankAccount = await _repository.GetById(id);

            // Checking if the user exists
            if (bankAccount is null) return null;

            // Automapping the userDTO
            var bankAccountDTO = _mapper.Map<UserBankAccountDTO>(bankAccount);

            // Deleting the user and saving the changes
            _repository.Delete(bankAccount);

            await _repository.Save();

            // Returning the deleted user
            return bankAccountDTO;
        }

        public bool Validate(UserBankAccountInsertDTO DTO)
        {
            // This user scope
            var nameExists = _repository.Search(u => u.Name == DTO.Name && u.UserId == DTO.UserId).Any();
            var accountNumberExists = _repository.Search(u => u.AccountNumber == DTO.AccountNumber && u.UserId == DTO.UserId).Any();

            if (accountNumberExists)
            {
                Errors.Add("The user already have this bank account associated.");
                return false;
            }

            if (nameExists)
            {
                Errors.Add("The user already have associated a bank account with this name.");
                return false;
            }

            return true;
        }

        public bool Validate(UserBankAccountUpdateDTO DTO)
        {
            var nameExists = _repository.Search(u => u.Name == DTO.Name && u.UserId == DTO.UserId).Any();
            var accountNumberExists = _repository.Search(u => u.AccountNumber == DTO.AccountNumber && u.UserId == DTO.UserId).Any();

            var isDifferentBankAccount = !_repository.Search(u => u.UserId == DTO.UserId && u.AccountNumber != DTO.AccountNumber).Any();

            if (accountNumberExists && !isDifferentBankAccount)
            {
                Errors.Add("The user already have this bank account associated.");
                return false;
            }

            if (nameExists && !isDifferentBankAccount)
            {
                Errors.Add("The user already have associated a bank account with this name.");
                return false;
            }

            return true;
        }
    }
}
