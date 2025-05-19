export interface Result{
    isSuccess: boolean;
    result:[Curso]
}

export interface Curso {
    id:number,
    idProfesor:string,
    createdDate:Date,
    titulo:string,
    descripcion:string,
    fechaInicio:Date | string,
    fechaFin:Date | string,
    nombreProfesor:string
}
