require "Common/define"


--管理器--
local transform;
local gameObject;
local LuaBehaviour;
local btnEnterGame;
local btnQuitGame;
local AcountLable;
local PassLable;


CLoginFrame     = {}
local this      = CLoginFrame;


--初始化面板--
function CLoginFrame.ReloadUI( obj )
	
	gameObject			= obj;
	transform			= gameObject.transform;
	warn("ReloadUI lua--->>"..gameObject.name );
	
    btnEnterGame   		= transform:FindChild("Login/StartGame").gameObject;
    AcountLable			= transform:FindChild("Login/Loginer/AcountInput/Input"):GetComponent('UILabel');
	PassLable			= transform:FindChild("Login/Loginer/PassInput/Input"):GetComponent('UILabel');
	warn("ReloadUI lua--->>"..btnEnterGame.name );
    
	LuaBehaviour 	 	= transform:GetComponent('CLuaBehaviour');
	LuaBehaviour:AddClick(btnEnterGame, this.ClickStartGame);
	
	--local goo = go.transform:FindChild('Label');
    --goo:GetComponent('UILabel').text = i;
	BundleManager:CreateObjByBundle('GameLogin', 4);
end

--单击事件--
function CLoginFrame.OnDestroy()

	warn("OnDestroy---->>>");
	btnEnterGame    = null;
    btnQuitGame     = null;
	
end


function CLoginFrame.ClickStartGame( obj )

	local acount = AcountLable.text;
	local pass   = PassLable.text;
	
	--local buff = ByteBuffer.New();
	--buff.WriteInt(1);			// 下消息ID
	--buff.WriteString(acount);
	--buff.WriteString(pass);
	--NetworkManager.SendMessage(buff);
	--warn("acount---->>>"..acount);
	--PanelManager:DestoryFrame("CLoginFrame");
	NetworkManager:SendConnect( acount, pass );
	--LevelManager:LoadLevel("pvp_004", "CityMainFrame");
	--GameEventManager:RegisterListenEvent(1, this.OnConnectedLoginApp );
	
end


function CLoginFrame.OnCreateFrame( )

	warn('CLoginFrame Create OK--->>>');
	
end
	
	
function CLoginFrame.OnConnectedLoginApp()

	warn('OnConnectedLoginApp--->>>');
end
	

