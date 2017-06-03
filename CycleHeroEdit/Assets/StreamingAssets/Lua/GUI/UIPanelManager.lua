require "Common/define"
require "Common/functions"







--管理器--
GameUIManager 	= {};
local this    	= GameUIManager;
local frameList ={};  

function GameUIManager.LuaScriptPanel()

	return  'CLoginFrame','CLoadLevelFrame','CityMainFrame','CWelComeFrame';
end


function GameUIManager.InitUIManagerOK()

	warn('UIManagerInit InitOK--->>>');
	PanelManager:CreatePanel('CLoginFrame', this.OnCreateFrame );
	
end


function GameUIManager.OnCreateFrame( )

	warn('CLoginFrame Create OK--->>>');
	
end



