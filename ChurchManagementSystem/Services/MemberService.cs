using DataAccess.Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MemberService: IMemberService
    {
        private readonly DataContext _context;

        public MemberService(DataContext context)
        {
            _context = context;
        }

        public async Task<Member> AddMemberAsync(Member member)
        {
            member.Id = Guid.NewGuid();
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return member;
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var existing = await _context.Members.FindAsync(member.Id);
            if (existing == null)
                throw new Exception("Member not found.");
            if (member.PicturePath==null)
            {
                member.PicturePath = existing.PicturePath;
            }
            _context.Entry(existing).CurrentValues.SetValues(member);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteMemberAsync(Guid id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member == null)
                return false;

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Member> GetMemberByIdAsync(Guid id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }
        public List<Member> GetAllMembers()
        {
            return  _context.Members.ToList();
        }
    }

    public interface IMemberService
    {
        Task<Member> AddMemberAsync(Member member);
        Task<Member> UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(Guid id);
        Task<Member> GetMemberByIdAsync(Guid id);
        Task<List<Member>> GetAllMembersAsync();
        List<Member> GetAllMembers();
    }
}
