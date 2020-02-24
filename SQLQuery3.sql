insert into Glucosa (H_A_Des,N_A_Des,H_D_Des,N_D_Des,H_A_Alm,N_A_Alm,H_D_Alm,N_D_Alm,H_A_Cen,N_A_Cen,H_D_Cen,N_D_Cen,H_A_Dor,N_A_Dor,H_D_Dor,H_D_Dor,FK_idUs,Fecha,Longitud,Latitud,Obs) 
values (H_A_Des, N_A_Des, H_D_Des, N_D_Des,H_A_Alm,N_A_Alm, H_D_Alm, N_D_Alm, H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, H_A_Dor, N_A_Dor, H_D_Dor, H_D_Dor, FK_idUs, Fecha, Longitud, Latitud, Obs);

insert into Glucosa (N_A_Alm) values (10);



select
	H_A_Des, N_A_Des, H_D_Des, N_D_Des,  
	H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, 
	H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, 
	H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, 
	FK_idUs, Fecha, Longitud, Latitud, Obs
	from Glucosa where fk_idUs = 1;

	select H_A_Des, N_A_Des, H_D_Des, N_D_Des, H_A_Alm, N_A_Alm, H_D_Alm, N_D_Alm, H_A_Cen, N_A_Cen, H_D_Cen, N_D_Cen, H_A_Dor, N_A_Dor, H_D_Dor, N_D_Dor, 	FK_idUs, Fecha, Longitud, Latitud, Obs, id	from Glucosa where fk_idUs = 1;