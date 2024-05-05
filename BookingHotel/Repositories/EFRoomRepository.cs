﻿using BookingHotel.Data;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingHotel.Repositories
{
    public class EFRoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public EFRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId, bool includeRoomType = false)
        {
            IQueryable<Room> query = _context.Rooms.Where(r => r.HotelId == hotelId);

            if (includeRoomType)
            {
                query = query.Include(r => r.RoomType);
            }

            return await query.ToListAsync();
        }
        public async Task AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}
