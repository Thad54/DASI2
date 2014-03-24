<?php
	header('Content-disposition: attachment; filename='.$_GET["Name"].'');
	header('Content-type: '.$_GET["Type"].'');
	readfile($_GET["Path"]);	
?>
