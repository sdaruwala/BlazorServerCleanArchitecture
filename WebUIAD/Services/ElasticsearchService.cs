using System;
using System.Threading.Tasks;
using Application.Common.Entities;
using Nest;

namespace WebUIAD.Services
{
    public class ElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
        }

        public async Task IndexDocumentAsync(PlayerDto playerDto)
        {
            try
            {
                await _elasticClient.IndexDocumentAsync(playerDto);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error indexing document: {ex.Message}");
                throw;
            }
        }

        public async Task<ISearchResponse<PlayerDto>> SearchAsync(string query)
        {
            try
            {
                return await _elasticClient.SearchAsync<PlayerDto>(s => s
                    .Index("shabbir")
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Name)
                            .Query(query)
                        )
                    )
                );
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error searching documents: {ex.Message}");
                throw;
            }
        }
    }
}
