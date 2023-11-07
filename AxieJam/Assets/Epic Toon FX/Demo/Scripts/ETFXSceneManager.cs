using UnityEngine;
using UnityEngine.SceneManagement;

namespace EpicToonFX
{

    public class ETFXSceneManager : MonoBehaviour
{
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	public bool GUIHide3 = false;	
	public bool GUIHide4 = false;
	
    public void LoadScene2DDemo()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_2ddemo");
	}
	public void LoadSceneCards()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_cards");
	}
    public void LoadSceneCombat()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_combat");
	}
	public void LoadSceneDecals()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_decals");
	}
	public void LoadSceneDecals2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_decals2");
	}
	public void LoadSceneEmojis()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_emojis");
	}
	public void LoadSceneEmojis2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_emojis2");
	}
	public void LoadSceneExplosions()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_explosions");
	}
	public void LoadSceneExplosions2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_explosions2");
	}
	public void LoadSceneFire()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_fire");
	}
	public void LoadSceneOnomatopoeia()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_onomatopoeia");
	}
	public void LoadSceneFireworks()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_fireworks");
	}
	public void LoadSceneFlares()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_flares");
	}
	public void LoadSceneMagic()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_magic");
	}
	public void LoadSceneMagic2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_magic2");
	}
	public void LoadSceneMagic3()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_magic3");
	}
	public void LoadSceneMainDemo()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_maindemo");
	}
	public void LoadSceneMissiles()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_missiles");
	}
	public void LoadScenePortals()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_portals");
	}
	public void LoadScenePortals2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_portals2");
	}
	public void LoadScenePowerups()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_powerups");
	}
	public void LoadScenePowerups2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_powerups2");
	}
	public void LoadScenePowerups3()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_powerups3");
	}
	public void LoadSceneSparkles()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_sparkles");
	}
	public void LoadSceneSwordCombat()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_swordcombat");
	}
	public void LoadSceneSwordCombat2()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_swordcombat2");
	}
	public void LoadSceneMoney()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_money");
	}
	public void LoadSceneHealing()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_healing");
	}
	public void LoadSceneWind()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_wind");
	}
	public void LoadSceneWater()  {
            UnityEngine.SceneManagement.SceneManager.LoadScene ("etfx_water");
	}
	
	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.L))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = true;
         }
     }
	      if(Input.GetKeyDown(KeyCode.J))
	 {
         GUIHide2 = !GUIHide2;
     
         if (GUIHide2)
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
         }
     }
		if(Input.GetKeyDown(KeyCode.H))
	 {
         GUIHide3 = !GUIHide3;
     
         if (GUIHide3)
		 {
             GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas> ().enabled = true;
         }
     }
	 	if(Input.GetKeyDown(KeyCode.K))
	 {
         GUIHide4 = !GUIHide4;
     
         if (GUIHide3)
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = true;
         }
     }
	}	
}
}