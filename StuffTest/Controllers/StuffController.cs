using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuffTest.Data.Abstract;
using StuffTest.Model;
using System.Linq;

namespace StuffTest.Controllers
{
    /// <summary>
    /// ���������� ���������
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StuffController : ControllerBase
    {


        private readonly ILogger<StuffController> _logger;
        private readonly IUserRepository _user;
        private readonly IPositionRepository _position;
        private readonly IMapper _mapper;
        /// <summary>
        /// ����������� ����������� ���������
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="position"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public StuffController(IUserRepository userRepository, IPositionRepository position, IMapper mapper, ILogger<StuffController> logger)
        {
            _logger = logger;
            _user = userRepository;
            _mapper = mapper;
            _position = position;
        }
        /// <summary>
        /// ��������� ������ ���� ������������� �������
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<StuffListModel> Get()
        {
            try
            {
                var data = _user.AllIncluding(x => x.Position);
                var userList = data.Select(_mapper.Map<UserModel>).ToList();
                return Ok(new StuffListModel { Users = userList });

            }
            catch (Exception e)
            {
                var model = new ErrorModel();
                model.Message = $"��� �� ����� �� ���. CorrelationId: {model.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {model.CorrelationId}", e);
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// ��������� ������ ������������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetSingle([FromRoute] Guid id)
        {
            try
            {
                
                var data = _user.GetSingle(x => x.Id == id);
                var position = _position.GetSingle(x => x.Id == data.PositionId);
                var user = _mapper.Map<UserModel>(data);
                user.Position = position.Name;
                return Ok(user);

            }
            catch (Exception e)
            {
                var model = new ErrorModel();
                model.Message = $"��� �� ����� �� ���. CorrelationId: {model.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {model.CorrelationId}", e);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// �������� ������������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<Guid> Delete([FromRoute] Guid id)
        {
            try
            {
                _user.DeleteWhere(x => x.Id == id);

                return Ok(id);

            }
            catch (Exception e)
            {
                var model = new ErrorModel();
                model.Message = $"��� �� ����� �� ���. CorrelationId: {model.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {model.CorrelationId}", e);
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="id">�������������</param>
        /// <param name="model">������ ������������</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Guid> Update([FromRoute] Guid id, [FromBody] UserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var position = _position.GetSingle(x => x.Name == model.Position);
                if (position == null) return BadRequest(new ErrorModel { Message = "���� �� �������" });
                var user = _user.GetSingle(x => x.Id == id);
                if (user == null) return BadRequest(new ErrorModel { Message = "������������ �� ������" });
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.PositionId = position.Id;

                _user.Update(user);
                _user.Commit();
                return Ok(id);

            }
            catch (DbUpdateException db)
            {
                var emodel = new ErrorModel();
                if (db.InnerException.Message.Contains("UNIQUE constraint failed: users.FirstName, users.LastName, users.MiddleName"))
                {

                    emodel.Message = $"������� ��� � �������� ������ ���� �����������. CorrelationId: {emodel.CorrelationId}";
                    _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", db);
                    return BadRequest(emodel);
                }
                emodel.Message = $"��� ������ �����. CorrelationId: {emodel.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", db);
                return BadRequest(emodel);
            }
            catch (Exception e)
            {
                var emodel = new ErrorModel();
                emodel.Message = $"��� �� ����� �� ���. CorrelationId: {emodel.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", e);
                return BadRequest(emodel);
            }
        }
        /// <summary>
        /// �������� ������������
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult<Guid>> Create([FromBody] UserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var position = _position.GetSingle(x => x.Name == model.Position);
                if (position == null) return BadRequest(new ErrorModel { Message = "���� �� �������" });
                _user.Add(new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    PositionId = position.Id

                });
                await _user.Commit();
                return Ok(model.Id);

            }
            catch (DbUpdateException db)
            {
                var emodel = new ErrorModel();
                if (db.InnerException.Message.Contains("UNIQUE constraint failed: users.FirstName, users.LastName, users.MiddleName"))
                {

                    emodel.Message = $"������� ��� � �������� ������ ���� �����������. CorrelationId: {emodel.CorrelationId}";
                    _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", db);
                    return BadRequest(emodel);
                }
                emodel.Message = $"��� ������ �����. CorrelationId: {emodel.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", db);
                return BadRequest(emodel);
            }
            catch (Exception e)
            {
                var emodel = new ErrorModel();
                emodel.Message = $"��� �� ����� �� ���. CorrelationId: {emodel.CorrelationId}";
                _logger.LogError($"��������� ������ CorrelationId: {emodel.CorrelationId}", e);
                return BadRequest(emodel);
            }
        }
    }
}