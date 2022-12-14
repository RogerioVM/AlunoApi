using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // Para autenticação, futuramente
//[Produces("application/json")] // opção para retornar no formato específicado
public class AlunosController : ControllerBase
{
    private IAlunoServices _alunoServices; // Feito manualmente
    public AlunosController(IAlunoServices alunoServices)
    {
        _alunoServices = alunoServices;
    }

    [HttpGet]
    /*
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    Acima são opções que podem ser usadas para específicar o retorno do 
    status code,mas não é muito indicado usar para não poluir o código 

    */
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
    {
        try
        {
            var alunos = await _alunoServices.GetAlunos();
            return Ok(alunos);
        }
        catch 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
        }
    }

    [HttpGet("AlunosPorNome")]
    public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByName([FromQuery] string nome)
    {
        try
        {
            var alunos = await _alunoServices.GetAlunosByNome(nome);

                if (alunos.Count() == 0)
                    return NotFound($"Não existem alunos com o nome = {nome}");

            return Ok(alunos);
        }
        catch
        {
            return BadRequest("Erro ao retornar aluno");
        }
    }

    [HttpGet("{id:int}", Name ="GetAluno")]
    public async Task<ActionResult<Aluno>> GetAluno(int id)
    {
        try
        {
            var aluno = await _alunoServices.GetAluno(id);

            if (aluno == null)
                return NotFound($"Não existe aluno com o id = {id}");

            return Ok(aluno);
        }
        catch
        {
            return BadRequest("Request Inválido");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Aluno>> Create(Aluno aluno)
    {
        try
        {
            await _alunoServices.CreateAluno(aluno);
            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno); // usado somente no POST
        }
        catch
        {
            return BadRequest("Request Inválido");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Aluno>> Edit(int id, [FromBody] Aluno aluno)
    {
        try
        {
            if(aluno.Id == id)
            {
                await _alunoServices.UpdateAluno(aluno);
                //return NoContent();
                return Ok($"O aluno com o id = {id} foi atualizado com sucesso.");
            }
            else
            {
                return BadRequest("Dados inconsistentes.");
            }
        }
        catch
        {
            return BadRequest("Request Inválido");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var aluno = await _alunoServices.GetAluno(id);
            if(aluno != null)
            {
                await _alunoServices.DeleteAluno(aluno);
                return Ok($"Aluno de id = {id} foi excluído com sucesso");
            }
            else
            {
                return NotFound($"Aluno com id = {id} nao foi encontrado");
            }   
        }
        catch
        {
            return BadRequest("Request Inválido");
        }
    }
  
}
