using Application.DTOs.QuestionarityDTO;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<Questionaire>> GetPacientsQuestionaires(int id)
        {
            return await context.Questionaire
                .Include(x => x.Pacient)
                .Where(x => x.Pacient.DoctorId == id)
                .ToListAsync();
        }

        public async Task<List<Questionaire>> GetQuestionaires(int id)
        {
            return await context.Questionaire.Where(x => x.PacientId == id).ToListAsync();
        }
    }
}
