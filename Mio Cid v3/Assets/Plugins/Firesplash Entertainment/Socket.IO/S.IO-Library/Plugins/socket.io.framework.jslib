mergeInto(LibraryManager.library, {
	InitializeSIOVars: function () {
		window.UnitySocketIOInstances = [];
		window.UnitySocketIOInstanceGameObjects = [];
		window.UnitySocketIOAuthPayloads = [];
	},
	
	CreateSIOInstance: function (instanceName, gameObjectName, targetAddress, enableReconnect) {
		var iName = UTF8ToString(instanceName);
		var goName = UTF8ToString(gameObjectName);
		
		window.UnitySocketIOInstanceGameObjects[instanceName] = gameObjectName;
		
		var fullAddress = UTF8ToString(targetAddress);
		var serverSepIndex = fullAddress.indexOf('/', 8);
		var serverAddress = (serverSepIndex > 0 ? fullAddress.substr(0, serverSepIndex) : fullAddress);
		var serverPath = (serverSepIndex > 0 ? fullAddress.substr(serverSepIndex) : '/socket.io/');
		
		var queryParams = {};
		if (fullAddress.indexOf('?') !== false) {
			var queryString = fullAddress.substring(fullAddress.indexOf('?') + 1).split('&');
			for(var i = 0; i < qs.length; i++){
				qs[i] = qs[i].split('=');
				queryParams[qs[i][0]] = decodeURIComponent(qs[i][1]);
			}
		}
		
		console.log('Connecting SIO to ' + serverAddress + ' on path ' + serverPath);
		window.UnitySocketIOAuthPayloads[iName] = null;
		window.UnitySocketIOInstances[iName] = window.io(serverAddress, {
			transports: ['websocket'],
			autoConnect: false,
			reconnection: (enableReconnect == 1),
			reconnectionDelay: 1000,
			reconnectionDelayMax: 8000,
			timeout: 5000,
			upgrade: true,
			rememberUpgrade: true,
			path: serverPath,
			query: queryParams,
			auth: function(cb) {
				cb(window.UnitySocketIOAuthPayloads[iName]);
			}
		});
		
		window.UnitySocketIOInstances[iName].on('connect', function() {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 1); //connected
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOSocketID', window.UnitySocketIOInstances[iName].id);
		});
		
		window.UnitySocketIOInstances[iName].on('disconnect', function(reason) {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 0); //disconnected
		});
		
		window.UnitySocketIOInstances[iName].on('reconnect', function(attemptNumber) {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 1); //connected
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOSocketID', window.UnitySocketIOInstances[iName].id);
		});
		
		window.UnitySocketIOInstances[iName].on('connect_timeout', function() {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 2); //errored
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'SIOWarningRelay', 'Timeout on connection ' + iName);
		});
		
		window.UnitySocketIOInstances[iName].on('connect_error', function(error) {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 2); //errored
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'SIOWarningRelay', 'Error on connection attempt for ' + iName + ': ' + error);
		});
		
		window.UnitySocketIOInstances[iName].on('reconnect_attempt', function() {
			window.UnitySocketIOInstances[iName].io.opts.transports = ['polling', 'websocket'];
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'SIOWarningRelay', 'Websocket failed for ' + iName + '. Trying to reconnect with polling enabled.');
		});
		
		window.UnitySocketIOInstances[iName].on('reconnect_error', function(error) {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 2); //errored
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'SIOWarningRelay', 'Error on reconnection attempt for ' + iName + ': ' + error);
		});
		
		window.UnitySocketIOInstances[iName].on('reconnect_failed', function(error) {
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'UpdateSIOStatus', 2); //errored
			SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'SIOWarningRelay', 'Reconnect failed for ' + iName + ': Max. attempts exceeded.');
		});
	},
	
	ConnectSIOInstance: function (instanceName, targetAddr, autoReconnect, payload) {
		var iName = UTF8ToString(instanceName);
		var connectPayload = UTF8ToString(payload);
		var targetAddress = UTF8ToString(targetAddr);
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined') {
			if (connectPayload != null && connectPayload.length > 1) {
				var connectPayloadObject = null;
				try {
					connectPayloadObject = JSON.parse(connectPayload);
					window.UnitySocketIOAuthPayloads[iName] = connectPayloadObject;
				} catch (err) {
					console.error('Error parsing Socket.IO authPayload on ' + iName + ': ' + err);
					window.UnitySocketIOAuthPayloads[iName] = null;
				}
			}
			if (window.UnitySocketIOInstances[iName].connected) window.UnitySocketIOInstances[iName].close();
			window.UnitySocketIOInstances[iName].io.uri = targetAddress;
			window.UnitySocketIOInstances[iName].io.opts.reconnection = (enableReconnect == 1);
			window.UnitySocketIOInstances[iName].connect();
		} else {
			console.error('The scripts on ' + iName + ' tried to connect a destroyed or not initialized Socket.IO instance. This should not happen.');
		}
	},
	
	CloseSIOInstance: function (instanceName) {
		var iName = UTF8ToString(instanceName);
		try {
			if (typeof window.UnitySocketIOInstances[iName] !== 'undefined' && window.UnitySocketIOInstances[iName] != null) {
				window.UnitySocketIOInstances[iName].close();
			}
		} catch(e) {
			console.warn('Exception while closing SocketIO connection on ' + iName + ': ' + e);
		}
	},
	
	DestroySIOInstance: function (instanceName) {
		var iName = UTF8ToString(instanceName);
		console.log('Destroying SIO instance ' + iName);
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined' && window.UnitySocketIOInstances[iName] != null) {
			window.UnitySocketIOInstances[iName].removeAllListeners();
		}
		delete window.UnitySocketIOInstances[iName];
		delete window.UnitySocketIOInstanceGameObjects[iName];
	},
	
	RegisterSIOEvent: function (instanceName, eventName) {
		var iName = UTF8ToString(instanceName);
		var eName = UTF8ToString(eventName);
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined') {
			window.UnitySocketIOInstances[iName].on(eName, function (data) {
				SendMessage(window.UnitySocketIOInstanceGameObjects[iName], 'RaiseSIOEvent', JSON.stringify({
					eventName: eName,
					data: (typeof data == 'undefined' ? null : (typeof data == 'string' ? data : JSON.stringify(data)))
				}));
			});
		} else {
			console.warn('The scripts on ' + iName + ' tried to register to an event on a destroyed or uninitialized Socket.IO instance. This should not happen.');
		}
	},
	
	UnregisterSIOEvent: function (instanceName, eventName) {
		var iName = UTF8ToString(instanceName);
		var eName = UTF8ToString(eventName);
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined') {
			window.UnitySocketIOInstances[iName].off(eName);
		} else {
			console.warn('The scripts on ' + iName + ' tried to unregister from an event on a destroyed or uninitialized Socket.IO instance. This should not happen.');
		}
	},
	
	SIOEmitNoData: function (instanceName, eventName) {
		var iName = UTF8ToString(instanceName);
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined') {
			window.UnitySocketIOInstances[iName].emit(UTF8ToString(eventName));
		} else {
			console.warn('The scripts on ' + iName + ' tried to emit data to an eighter closed or never connected Socket.IO instance. This should not happen.');
		}
	},
	
	SIOEmitWithData: function (instanceName, eventName, data, parseAsJSON) {
		var iName = UTF8ToString(instanceName);
		var parsedData = "__ERROR__";
		if (typeof window.UnitySocketIOInstances[iName] !== 'undefined') {
			if (parseAsJSON == 1) {
				parsedData = JSON.parse(UTF8ToString(data));
			}
			else 
			{
				parsedData = UTF8ToString(data)
			}
			window.UnitySocketIOInstances[iName].emit(UTF8ToString(eventName), parsedData);
		} else {
			console.warn('The scripts on ' + iName + ' tried to emit data to an eighter closed or never connected Socket.IO instance. This should not happen.');
		}
	}
});
