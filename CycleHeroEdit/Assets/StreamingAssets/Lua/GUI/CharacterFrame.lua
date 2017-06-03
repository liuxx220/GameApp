require "Common/define"


--管理器--
local transform;
local gameObject;
local LuaBehaviour;
local btnEnterGame;
local btnQuitGame;



CharacterFrame     = {}
local this         = CharacterFrame;


--初始化面板--
function CharacterFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );
	
    this.btnEnterGame   = transform:FindChild("Login/StartGame").gameObject;
    this.btnQuitGame    = transform:FindChild("Login/QuetGame").gameObject;
    
	LuaBehaviour 	 	= transform:GetComponent('CLuaBehaviour');
	LuaBehaviour:AddClick(btnEnterGame, this.ClickStartGame);
	LuaBehaviour:AddClick(btnQuitGame, this.ClickQuestGame);
 
	--local goo = go.transform:FindChild('Label');
    --goo:GetComponent('UILabel').text = i;
end

--单击事件--
function CharacterFrame.OnDestroy()

	warn("OnDestroy---->>>");
	btnEnterGame    = null;
    btnQuitGame     = null;
	
end


function CharacterFrame.ClickStartGame( obj )

	PanelManager:DestoryFrame("CLoginFrame");
	LevelManager:LoadLevel("");
end


function CharacterFrame.ClickQuestGame( obj )

	PanelManager:DestoryFrame("CLoginFrame");
	LevelManager:LoadLevel("");
	
end
	
	

