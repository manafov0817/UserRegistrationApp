using Microsoft.EntityFrameworkCore.Migrations;

namespace UserRegistirationApp.Data.Migrations
{
    public partial class AddProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                 @"create or replace function user_insert(name character varying,surname character varying,username character varying,email character varying,password character varying)
                    returns int as 
                    $$
                    begin 
	                    insert into Users(Name,Surname,UserName,Email,Password )
	                    values(name,surname,username,email,password );
	                    if found then 
		                    return 1;
	                    else return 0;
	                    end if;
                    end
                    $$
                    language plpgsql");

            migrationBuilder.Sql(
             @"create or replace function user_update(_id int, _name character varying, _surname character varying, _username character varying, _email character varying, _password character varying)
                    returns int as 
                    $$
                    begin 
	                    update Users
	                    set 
		                    Name = _name,
		                    Surname = _surname,
		                    UserName = _username,
		                    Email = _email,
		                    Password = _password
	                    where UserId = _id;
	                    if found then 
		                    return 1;
	                    else return 0;
	                    end if;
                    end 
                    $$
                    language plpgsql");

            migrationBuilder.Sql(
            @"create or replace function user_select()
                    returns table
                    (
                    _id int,
                    _name character varying,
                    _surname character varying,
                    _username character varying,
                    _email character varying,
                    _password character varying
                    )
                    as 
                    $$
                    begin
	                    return query
	                    select UserId, Name, Surname, UserName, Email, Password from Users order by UserId;
	                    end
                    $$
                    language plpgsql");


            migrationBuilder.Sql(
            @"create or replace function user_select_by_id(_userid int)
                    returns table
                    (
                    _id int,
                    _name character varying,
                    _surname character varying,
                    _username character varying,
                    _email character varying,
                    _password character varying
                    )
                    as 
                    $$
                    begin
	                    return query
	                    select UserId, Name, Surname, UserName, Email, Password from Users  
                        where UserId = _userid;
	                    end
                    $$
                    language plpgsql");

            migrationBuilder.Sql(
              @"create or replace function username_exsists( _username character varying)
                    returns SETOF  bigint as 
                    $$ 
                    begin   
   	                    return query  select count(*) as count from Users u where u.UserName = _username;
                     end;
                    $$
 
                    language plpgsql");

            migrationBuilder.Sql(
               @"create or replace function userid_exists(_id int)
                    returns  SETOF  bigint as 
                    $$
                    begin
   	                    return query  select count(*) as count from Users u where u.UserId = _id;
                    end;
                    $$
                    language plpgsql");

            migrationBuilder.Sql(
               @"create or replace function user_delete(_id int)
                        returns int as 
                        $$
                        begin
	                        delete from Users
	                        where UserId=_id;
	                        if found then
		                        return 1;
	                        else return 0;
	                        end if;
                        end 
                        $$
                        language plpgsql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
