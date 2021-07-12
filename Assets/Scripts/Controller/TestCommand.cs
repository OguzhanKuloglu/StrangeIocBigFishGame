using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class TestCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("*** TEST COMMAND ***");
        }
    }
}