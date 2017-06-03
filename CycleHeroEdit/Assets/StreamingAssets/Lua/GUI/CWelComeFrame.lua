require "Common/define"


--管理器--
local transform;
local gameObject;
local LuaBehaviour;
local btnEnterGame;




CWelComeFrame     = {}
local this      = CWelComeFrame;


--初始化面板--
function CWelComeFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );
	
    btnEnterGame   		= transform:FindChild("Login/StartGame").gameObject;
   
	warn("ReloadUI lua--->>"..btnEnterGame.name );
    
	LuaBehaviour 	 	= transform:GetComponent('CLuaBehaviour');
	LuaBehaviour:AddClick(btnEnterGame, this.ClickStartGame);
	
	--local goo = go.transform:FindChild('Label');
    --goo:GetComponent('UILabel').text = i;
end

--单击事件--
function CWelComeFrame.OnDestroy()

	warn("OnDestroy---->>>");
	btnEnterGame    = null;
    btnQuitGame     = null;
	
end


function CWelComeFrame.ClickStartGame( obj )

	PanelManager:DestoryFrame("CWelComeFrame");
end

	
	

