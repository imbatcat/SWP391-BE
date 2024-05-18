CREATE DATABASE [SWP399-PHCS]
USE [SWP399-PHCS]

DROP TABLE [Role]
DROP TABLE Account
DROP TABLE Feedback
DROP TABLE Pet
DROP TABLE Veterinarian
DROP TABLE TimeSlot
DROP TABLE PageInformation
DROP TABLE Appointment

CREATE TABLE [Role](
	role_id INT PRIMARY KEY NOT NULL,
	role_name NVARCHAR(50) CHECK(role_name <= 50),
)

CREATE TABLE Account (
	account_id CHAR(10) PRIMARY KEY NOT NULL,   --COMPLEX NONE AUTO INCREMENT
	role_id INT NOT NULL,
	ac_gender VARCHAR(50) NOT NULL /*CHECK(ac_gender like 'female' or ac_gender like 'male' or ac_gender like 'other')*/,
	ac_dob DATE NOT NULL DEFAULT getDate(),
	phone_number CHAR(10) CHECK(phone_number < 10),
	email NVARCHAR(50) NOT NULL UNIQUE,
	username NVARCHAR(50) NOT NULL UNIQUE,
	[password] CHAR(60) NOT NULL  CHECK([password] < 60),
	fullname NVARCHAR(100) NOT NULL,
	is_disabled BIT, 
	join_date DATE DEFAULT getDate(),
	FOREIGN KEY (role_id) REFERENCES [Role](role_id),
)

CREATE TABLE InformationType(
	info_type_id INT PRIMARY KEY,
	info_type_name NVARCHAR(50),
)


CREATE TABLE PageInformation(
	page_info_id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	page_info_title NVARCHAR(500),
	info_type_id INT FOREIGN KEY REFERENCES InformationType(info_type_id),
)

CREATE TABLE Feedback(
	feedback_id CHAR(10) PRIMARY KEY NOT NULL,
	account_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES Account(account_id),
	rating INT CHECK(rating <= 5 OR rating >= 1) DEFAULT 5,
	[description] NVARCHAR(100) CHECK([description] <= 500),
)

CREATE TABLE Pet(
	pet_id CHAR(10) PRIMARY KEY NOT NULL,   --COMPLEX  NONE AUTO INCREMENT
	account_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES Account(account_id),
	pet_img NVARCHAR(600),
	pet_name NVARCHAR(50) CHECK(pet_name <=50),
	pet_gender NVARCHAR(50) NOT NULL,
	pet_age INT,
	species NVARCHAR(50) ,
	breed NVARCHAR(50) CHECK(breed <= 50),
	vaccination NVARCHAR(100) NOT NULL DEFAULT 'None', 
	is_hidden BIT,
)

CREATE TABLE VeterinarySpecialty(
	specialty_id INT PRIMARY KEY IDENTITY(1,1),
	specialty_name NVARCHAR(50),
)

CREATE TABLE Veterinarian(
	vet_id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	vet_img NVARCHAR(600),
	vet_experience NVARCHAR(600) DEFAULT 'No infomation',
	vet_position NVARCHAR(50) NOT NULL,
	account_id CHAR(10) REFERENCES Account(account_id),
	specialty_id INT REFERENCES VeterinarySpecialty(specialty_id),
)

CREATE TABLE TimeSlot(
	time_slot_id INT PRIMARY KEY NOT NULL,
	time_slot_from NVARCHAR(50) NOT NULL,
	time_slot_to NVARCHAR(50),
	current_customer INT CHECK(current_customer <= 10),
)

CREATE TABLE Appointment (     -- 1 to 1 BookingPAyment  , medicalRecord
	appointment_id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	app_date DATE NOT NULL,
	owner_name NVARCHAR(100) NOT NULL,
	owner_phone CHAR(10) CHECK(owner_phone < 10),
	appointment_type NVARCHAR(50),
	pet_age INT,
	pet_gender NVARCHAR(50) NOT NULL,
	app_note NVARCHAR(500) CHECK(app_note <= 500),
	booking_price MONEY NOT NULL,
	app_vaccination DATETIME NOT NULL,
	app_species NVARCHAR(200) NOT NULL,
	app_breed NVARCHAR(50),
	app_status NVARCHAR(50) NOT NULL DEFAULT 'Unknown',
	account_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES Account(account_id),
	pet_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES Pet(pet_id),
	vet_id INT NOT NULL FOREIGN KEY REFERENCES Veterinarian(vet_id),
	time_slot_id INT NOT NULL FOREIGN KEY REFERENCES TimeSlot(time_slot_id),
)

CREATE TABLE BookingPayment(  --1 to 1  Appointment
	payment_id INT ,
	appointment_id INT  NOT NULL FOREIGN KEY REFERENCES Appointment(appointment_id),
	PRIMARY KEY (payment_id,appointment_id),
	payment_amount MONEY,
	payment_date DATE NOT NULL,
	payment_method NVARCHAR(50),
)

CREATE TABLE MedicalRecord(  -- 1 to 1 appointment 
	medical_record_id CHAR(10) PRIMARY KEY NOT NULL,
	pet_name NVARCHAR(50),
	date_created DATE DEFAULT getDate(),
	pet_id CHAR(10) REFERENCES Pet(pet_id),
	vet_fullname NVARCHAR(50) CHECK(vet_fullname <= 50),
	appointment_id INT NOT NULL REFERENCES Appointment(appointment_id),
	pet_weight FLOAT ,
	pet_symptoms NVARCHAR(200) NOT NULL CHECK(pet_symptoms <= 200),
	pet_allergy NVARCHAR(200) NOT NULL DEFAULT 'None',
	diagnosis NVARCHAR(200),
	med_additional_note NVARCHAR(500),
	follow_up_app DATE,
	follow_up_note NVARCHAR(200),
	--PRIMARY KEY(medical_record_id,appointment_id),      -- 2 PRIMARY KEY FOR 1-1
)

CREATE TABLE ServiceOrder (
	service_order_id CHAR(10) PRIMARY KEY NOT NULL,
	service_order_money MONEY,
	medical_record_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES MedicalRecord(medical_record_id),
	service_oder_status NVARCHAR(50),
	order_data DATETIME,
)
	
CREATE TABLE ServicePayment(
	service_payment_id INT ,
	service_order_id CHAR(10) REFERENCES ServiceOrder(service_order_id),
	PRIMARY KEY(service_payment_id,service_order_id),
	service_payment_amount MONEY,
	service_payment_date DATE NOT NULL,
	payment_method NVARCHAR(50),
)

CREATE TABLE [Service] (
	service_id INT PRIMARY KEY  NOT NULL IDENTITY(1,1),
	service_price MONEY,
	[service_name] NVARCHAR(50),
)

CREATE TABLE ServiceOderDetail (
	service_order_id CHAR(10) NOT NULL REFERENCES ServiceOrder(service_order_id),
	service_id INT NOT NULL REFERENCES [Service](service_id),
	PRIMARY KEY (service_order_id,service_id),
)

CREATE TABLE Cage(
	cage_id INT PRIMARY KEY NOT NULL,
	cage_number INT,
	is_occupied BIT,
)

CREATE TABLE AdmissionRecord (
	admission_record_id CHAR(50) PRIMARY KEY NOT NULL,
	admission_date DATETIME NOT NULL,
	discharge_date DATETIME,
	medical_record_id CHAR(10) FOREIGN KEY REFERENCES MedicalRecord(medical_record_id),
	is_discharged BIT,
	pet_health_status NVARCHAR(50),
	cage_id INT FOREIGN KEY REFERENCES Cage(cage_id),
)

CREATE TABLE PetHealthTracker(
	pet_health_tracker_id CHAR(10) PRIMARY KEY NOT NULL,
	admission_record_id CHAR(50) NOT NULL FOREIGN KEY REFERENCES AdmissionRecord(admission_record_id),
	pet_name INT,
	tracker_date DATE,
	tracker_description NVARCHAR(200),
)

CREATE TABLE Drug(
	drug_id CHAR(10) PRIMARY KEY,
	drug_info NVARCHAR(50),
	drug_guide NVARCHAR(200),
	drug_name NVARCHAR(50),
)

CREATE TABLE Prescription(
	prescription_id CHAR(10) PRIMARY KEY,
	vet_fullname NVARCHAR(50),
	pres_date_created DATE,
	pet_name NVARCHAR(50),
	owner_fullname NVARCHAR(50),
	medical_record_id CHAR(10) NOT NULL FOREIGN KEY REFERENCES MedicalRecord(medical_record_id),
	drug_id CHAR(10) FOREIGN KEY REFERENCES PrescriptionDetail(drug_id)
)

CREATE TABLE PrescriptionDetail(
	prescription_id CHAR(10) REFERENCES Prescription(prescription_id),
	drug_id CHAR(10) REFERENCES Drug(drug_id),
)