using UnityEngine;

namespace ThirdPersonShooter.UI
{
	public class GameOverMenu : MenuBase
	{
		public override string ID => "Game Over";
		
		public override void OnOpenMenu(UIManager _manager)
		{
			Time.timeScale = 0;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public override void OnCloseMenu(UIManager _manager)
		{
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		
		public void OnClickMainMenu() => GameManager.Instance.LevelManager.UnloadGame(() => UIManager.ShowMenu("Main", false));
	}
}