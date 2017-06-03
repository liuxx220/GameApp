require "Common/define"
require "Common/functions"







--管理器--
NetworkManager 	= {};
local this    	= NetworkManager;
local frameList ={};  

function NetworkManager.LuaScriptPanel()

	return  'CLoginFrame','CLoadLevelFrame','CityMainFrame','CWelComeFrame';
end


function NetworkManager.InitUIManagerOK()

	warn('UIManagerInit InitOK--->>>');
	PanelManager:CreatePanel('CLoginFrame', this.OnCreateFrame );
	
end


function NetworkManager.OnCreateFrame( )

	warn('CLoginFrame Create OK--->>>');
	
end



