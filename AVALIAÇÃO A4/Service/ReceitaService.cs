using AVALIAÇÃO_A4.Classes;
using AVALIAÇÃO_A4.DataBase;
using Microsoft.EntityFrameworkCore;

namespace AVALIAÇÃO_A4.Componentes
{
    public class ReceitaService
    {
        private readonly DbContextToMemory dbContext;

        public ReceitaService(DbContextToMemory db)
        {
            dbContext = db;
        }

        public Receita Insert(Receita vo)
        {
            /*if (dto.Description == null)
            {
                throw new BadRequestException("Dados inválidos");
            }
            if (dto.Barcodetype == null)
            {
                throw new BadRequestException("Dados inválidos");
            }
            if (dto.Barcodetype == null)
            {
                throw new BadRequestException("Dados inválidos");
            }*/

            //var agendamento = _mapper.Map<TbProduct>(dto);

            dbContext.Receita.Add(vo);
            dbContext.SaveChanges();

            /*_stockLogService.InsertStockLog(new StockLogDTO
            {
                Productid = product.Id,
                Qty = product.Stock,
                Createdat = DateTime.Now
            });*/

            return vo;
        }

    }
}
