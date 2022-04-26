using Application.DTOs.QuestionarityDTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.QuestionaryService
{
    public interface IQuestionaryService
    {
        Task<List<Questionaire>> GetQuestionaires(int id);

        Task<List<Questionaire>> CreateQuestionairy(int id, QuestionarityDTO questionarity);
    }
}
