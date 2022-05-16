using Application.DTOs.QuestionarityDTO;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.QuestionaryService
{
    public class QuestionaryService : BaseService, IQuestionaryService
    {
        public QuestionaryService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<Questionaire>> CreateQuestionairy(int id, QuestionarityDTO questionarity)
        {
            var questionaryDB = mapper.Map<Questionaire>(questionarity);
            questionaryDB.PacientId = id;
            context.Questionaire.Add(questionaryDB);
            await context.SaveChangesAsync();
            return await GetQuestionaires(id);
        }

        public async Task<List<PacientsQuestionarityDTO>> GetPacientsQuestionaires(int id)
        {
            return await context.Questionaire
                .Include(x => x.Pacient)
                .Where(x => x.Pacient.DoctorId == id)
                .Select(x=>new PacientsQuestionarityDTO
                {
                    Fullname = x.Pacient.FullName,
                    Comments =x.Comments,
                    Temperature = x.Temperature,
                    QDate = x.Date,
                    Headache = x.Headache,
                    ObstructedBreathing = x.ObstructedBreathing,
                    Tiredness = x.Tiredness
                })
                .ToListAsync();
        }

        public async Task<List<Questionaire>> GetQuestionaires(int id)
        {
            return await context.Questionaire.Where(x => x.PacientId == id).ToListAsync();
        }
    }
}
