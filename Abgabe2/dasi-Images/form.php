<div id="login">
			<img src="art assets/images/dancies_140_transp.png"/>
			<form action="index.php" method="POST">
				<p>Username</p>
				<input type="text" name="Name" value></input>
				<p>Password</p>
				<input type="password" name="Password"></input><br/>
				
				<input type="submit" id="submit" name="submit" value="Login"><br/>

			</form>
			<a href="javascript:void()" onclick="ShowForm()"><p id="show">Register New Account</p></a>
			<div id="register_form">
				<form action="save.php" method="Post" name="ContactForm" id="reg_form" action="post" enctype="multipart/form-data" onsubmit="return ValidateForm();">
					<p class="inputText"> Name </p>
					<input type="text" name="RegisterName"><br/>
					<p class="inputText"> Surname </P>
					<input type="text" name="RegisterSurname"><br/>
					<p class="inputText"> Username </P>
					<input type="text" name="RegisterUsername"><br/>
					<p class="inputText"> E-Mail </P>
					<input type="text" name="RegisterEmail"><br/>
					<p class="inputText"> Password </P>
					<input type="password" id="password" name="RegisterPassword"><img class="reg_icon wrong" src='art assets/images/Windows-Close-Program-ico.png'><img class="reg_icon right" src='art assets/images/check-2-icon.png'><br/>
					<p class="inputText"> Confirm Password </P>
					<input type="password" id="condirmation" oninput="checkPassword(this)" ><img class="reg_icon2 right" src='art assets/images/check-2-icon.png'><img class="reg_icon2 wrong" src='art assets/images/Windows-Close-Program-ico.png'><br/>
					<p>Avatar</p>
					<input type="hidden" name="MAX_FILE_SIZE" value="1024000">
					<input id="dropzone" type="file" name="files" >
					<input type="submit" value="submit">
				</form>	
			</div>
		
		</div>