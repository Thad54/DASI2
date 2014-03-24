<?php
	error_reporting(0);
	function authenticate($name,$password){
		$host   = 'localhost'; 
		$dbUser = 'root'; 
		$dbPass = ''; 
		$dbName = 'webtop'; 

		$md = md5($password);
		$db = @new mysqli($host, $dbUser, $dbPass, $dbName);
		if(mysqli_connect_errno() == 0){
			$sql = "SELECT `username` FROM `user` WHERE `username`=? AND `pwd`=?";
			$result = $db->prepare($sql);
			$result->bind_param('ss',$name, $md);
			$result->execute();
			$result->bind_result($check);
			$check = $result->fetch();
		$db->close();	
		} else {
			return false;
		}
		
		if($check != ""){
			$login = true;
		}else{
			$login = false;
		}
		return($login);


	// MySQLi	
	}
?>	