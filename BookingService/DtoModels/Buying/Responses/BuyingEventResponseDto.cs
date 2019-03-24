namespace BookingService.DtoModels.Buying.Responses
{
    public class BuyingEventResponseDto : BuyingResponseDto
    {
        public int BuyingId { get; }
        public int BookingId { get; }        
    }
}
