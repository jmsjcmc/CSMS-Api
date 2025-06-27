using AutoMapper;
using CSMapi.Models;

namespace CSMapi.Helpers
{
    public class DocumentHelper 
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DocumentHelper(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Document> adddocument(DocumentRequest request)
        {
            var document = _mapper.Map<Document>(request);
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }
    }
}
