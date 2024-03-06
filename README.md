# BarberShop

![image](https://github.com/makhammadsoliyev/BarberShop/assets/149594973/a87d7a81-f8f4-4dad-b20f-705fe24ab5fb)
<br>
<bold>
UserService
	1. RegisterAsync()
	2. LogInAsync(phone, password)
	3. GetAllAsync()
	4. GetByIdAsync(id)
	5. DeleteAsync(id)
	6. UpdateAsync(id, user)
	7. GetAllAppointmentsAsync()

BarbershopService
	1. CreateAsync(barbershop)
	2. GetByIdAsync(id)
	3. UpdateAsync(id, barbershop)
	4. DeleteAsync(id)
	5. GetAllAsync()
	6. GetAllAppointmentsAsync()

SearchService
	1. SearchBarbershopByLocation(lat, long, barbershops)
	2. SearchBarbershopByCity(city, barbershops)

AppointmentService
	1. BookAsync(Appointment)
	2. CancelAsync(Id) // Delete

RegisterMenu
	UserService.RegisterAsync()
LogInMenu
	UserService.LogInAsync(phone, password) // it will check this user registered or not Or Check this user admin or not
	AdminMenu
		UserService.GetAllAsync()
			   .GetByIdAsync(id)
		BarbershopService
			1. CreateAsync(barbershop)
			2. GetByIdAsync(id)
			3. UpdateAsync(id, barbershop)
			4. DeleteAsync(id)
			5. GetAllAsync()
			6. GetAllAppointmentsAsync()
	UserMenu
		UserService
			3. UpdateAsync(id, barbershop)
			4. DeleteAsync(id)
			6. GetAllAppointmentsAsync()
		AppointmentMenu
			AppointmentService
				1. BookAsync(Appointment)
					SearchService
						1. SearchBarbershopByLocation(lat, long, barbershops)
						2. SearchBarbershopByCity(city, barbershops)
				2. CancelAsync(Id) // Delete
</bold>