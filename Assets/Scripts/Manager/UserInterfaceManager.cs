using System.Collections.Generic;
using Resource;
using UnityEngine;
using UserInterface;

namespace Manager
{
    public class UserInterfaceManager : MonoBehaviour
    {
        public QuickTaskBar quickTaskBar;

        public void UpdateQuickTaskBar(List<ResourceNode> entities)
        {
            quickTaskBar.UpdateData(entities);
        }
    }
}