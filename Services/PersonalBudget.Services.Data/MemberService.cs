namespace PersonalBudget.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PersonalBudget.Data.Common.Repositories;
    using PersonalBudget.Data.Models;
    using PersonalBudget.Services.Mapping;
    using PersonalBudget.Web.ViewModels.Members;

    public class MemberService : IMemberService
    {
        private readonly IDeletableEntityRepository<Member> memberRepository;

        public MemberService(IDeletableEntityRepository<Member> memberRepository)
        {
            this.memberRepository = memberRepository;
        }

        public async Task CreateMember(CreateMemberInputModel createMember, string userId)
        {
            var member = new Member
            {
                Name = createMember.Name,
                UserId = userId,
            };
            await this.memberRepository.AddAsync(member);
            await this.memberRepository.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var member = this.memberRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            this.memberRepository.Delete(member);
            await this.memberRepository.SaveChangesAsync();
        }

        public async Task Edit(MemberViewModel editMember, string userId, int id)
        {
            var member = this.memberRepository.AllAsNoTracking()
    .Where(x => x.UserId == userId & x.Id == id).FirstOrDefault();
            member.Name = editMember.Name;
            this.memberRepository.Update(member);
            await this.memberRepository.SaveChangesAsync();
        }

        public bool Exists(string userId, string name)
        {
            var member = this.memberRepository.AllAsNoTracking().Where(x => x.UserId == userId & x.Name == name).FirstOrDefault();
            if (member == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<T> GetAll<T>(string userId)
        {
            var members = this.memberRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .To<T>()
                .ToList();
            return members;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllMembers(string userId)
        {
            return this.memberRepository.All()
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public T GetById<T>(string userId, int id)
        {
            var member = this.memberRepository.AllAsNoTracking()
            .Where(x => x.UserId == userId & x.Id == id)
            .To<T>()
            .FirstOrDefault();

            return member;
        }
    }
}
