using GoldenRaspberryAwards.Api.Models;

namespace GoldenRaspberryAwards.Api.Responses
{
    public class MovieProducerResponse
    {
        public IEnumerable<MovieProducer> Min { get; set; }
        public IEnumerable<MovieProducer> Max { get; set; }

        public MovieProducerResponse(IEnumerable<MovieProducer> min, IEnumerable<MovieProducer> max)
        {
            Min = min;
            Max = max;
        }
    }
}