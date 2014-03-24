<?php
	session_start();
	echo"success";
	session_destroy();
	setcookie ("User","", time()-1800);
	header("Location: index.php"); 
	
?>