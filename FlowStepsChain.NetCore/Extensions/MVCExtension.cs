using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FlowStepsChain.NetCore.Extensions
{
    public class ErroState
    {
        public string Field { get; set; }
        public string Text { get; set; }
    }
    public static class MVCExtension
    {
        public static string MontaErrorMessage(this ModelStateDictionary _ModelState)
        {
            var xMessage = "";

            if (_ModelState.Values.Count() > 0)
            {
                var _lista = _ModelState.Values.ToList();
                for (int i = 0; i < _lista.Count; i++)
                {
                    if (_lista[i].Errors.Count > 0)
                    {
                        xMessage += _lista[i].Errors[0].ErrorMessage + "<br />";
                    }
                }
            }

            return xMessage;
        }

        public static List<ErroState> ModelStateErroList(this ModelStateDictionary _ModelState)
        {

            List<ErroState> lista = new List<ErroState>();

            if (_ModelState.Values.Count() > 0)
            {
                var _lista = _ModelState.Values.ToList();
                for (int i = 0; i < _lista.Count; i++)
                {
                    if (_lista[i].Errors.Count > 0)
                    {
                        lista.Add(new ErroState()
                        {
                            Field = "Custom",
                            Text = _lista[i].Errors[0].ErrorMessage
                        });
                    }
                }
            }

            return lista;
        }
        public static string MontaErrorMessageJson(this ModelStateDictionary _ModelState)
        {
            var model = new List<ErroState>();

            if (_ModelState.Values.Count() > 0)
            {
                var keys = _ModelState.Keys.ToList();
                var _lista = _ModelState.Values.ToList();
                for (int i = 0; i < _lista.Count; i++)
                {
                    if (_lista[i].Errors.Count > 0)
                    {
                        model.Add(new ErroState
                        {
                            Text = _lista[i].Errors[0].ErrorMessage,
                            Field = keys[i].Contains(".") ? keys[i].Replace(".", "_") : keys[i]
                        });
                    }
                }
            }

            return JsonConvert.SerializeObject(model);
        }
        public static List<string> ModelStateErroListString(ModelStateDictionary _ModelState)
        {

            List<string> lista = new List<string>();

            if (_ModelState.Values.Count() > 0)
            {
                var _lista = _ModelState.Values.ToList();
                for (int i = 0; i < _lista.Count; i++)
                {
                    if (_lista[i].Errors.Count > 0)
                    {
                        lista.Add(_lista[i].Errors[0].ErrorMessage);
                    }
                }
            }

            return lista;
        }

        public static void ModelStateIgnoreError(ModelStateDictionary _ModelState, string _Key, bool notContains)
        {
            try
            {
                if (notContains)
                    foreach (var key in _ModelState.Keys.Where(key => key.Contains(_Key)).ToList())
                        _ModelState[key].Errors.Clear();
                else
                    foreach (var key in _ModelState.Keys.Where(key => !key.Contains(_Key)).ToList())
                        _ModelState[key].Errors.Clear();
            }
            catch { }
        }
    }
}
