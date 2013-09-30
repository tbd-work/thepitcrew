using System;
using Availability.Domain.Model.Rooms;
using Common.Kernel;
using FluentAssertions;
using Xunit;

namespace Availability.Tests
{
    public class When_creating_room_availability
    {
        private readonly RoomAvailability _roomAvailability;

        public When_creating_room_availability()
        {
            _roomAvailability = RoomAvailability.MakeAvailability(RoomType.SingleRoom, DateTime.Now, 10);
        }

        [Fact]
        public void Should_set_current_availability()
        {
            _roomAvailability.Availability.Should().Be(10);
        }
    }
}